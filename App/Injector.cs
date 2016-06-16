using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace App
{
    class Injector
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
            uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess,
            IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        const int PROCESS_CREATE_THREAD = 0x0002;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_READ = 0x0010;

        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_READWRITE = 4;

        public static Process TargetProcess { get; set; }

        internal static void Inject()
        {
            Log.I("DFA 모듈 로드중...");

            foreach (ProcessModule mod in TargetProcess.Modules)
            {
                if (mod.ModuleName == Path.GetFileName(Global.DLL_PATH))
                {
                    Log.E("DFA 모듈이 이미 로드되어 있습니다.");
                    return;
                }
            }

            string fullpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Global.DLL_PATH);

            IntPtr procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, TargetProcess.Id);
            IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            IntPtr allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, (uint)((fullpath.Length + 1) * Marshal.SizeOf(typeof(char))), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
            UIntPtr bytesWritten;
            WriteProcessMemory(procHandle, allocMemAddress, Encoding.Default.GetBytes(fullpath), (uint)((fullpath.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);
            CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);
            Log.S("DFA 모듈 로드됨.");
        }

        internal static void Eject()
        {
            Log.I("DFA 모듈 언로드중...");

            IntPtr procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, TargetProcess.Id);
            IntPtr freeLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "FreeLibrary");

            foreach (ProcessModule mod in TargetProcess.Modules)
            {
                if (mod.ModuleName == Path.GetFileName(Global.DLL_PATH))
                {
                    Log.D("DFA 모듈 언로드: 0x{0:X}", mod.BaseAddress);
                    CreateRemoteThread(procHandle, IntPtr.Zero, 0, freeLibraryAddr, mod.BaseAddress, 0, IntPtr.Zero);
                }
            }

            Log.S("DFA 모듈 언로드됨.");
        }
    }
}

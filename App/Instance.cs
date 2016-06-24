using System.Collections.Generic;

namespace App
{
    class Instance
    {
        public string Name { get; }
        public byte Tank { get; }
        public byte DPS { get; }
        public byte Healer { get; }

        public Instance(string name, byte tank, byte healer, byte dps)
        {
            Name = name;
            Tank = tank;
            DPS = dps;
            Healer = healer;
        }
    }

    class InstanceList
    {
        private static Dictionary<ushort, Instance> instanceDict = new Dictionary<ushort, Instance>()
        {
            // 오류
            { 0, new Instance("알 수 없음", 0, 0, 0) },
            { 65535, new Instance("오류 발생!", 0, 0, 0) },


            // 던전 (2.x)
            { 169, new Instance("토토라크 감옥", 1, 1, 2) },
            { 164, new Instance("탐타라 묘소", 1, 1, 2) },
            { 161, new Instance("구리종 광산", 1, 1, 2) },
            { 157, new Instance("사스타샤 침식 동굴", 1, 1, 2) },
            { 172, new Instance("금빛 골짜기", 1, 1, 2) },
            { 166, new Instance("하우케타 별궁", 1, 1, 2) },
            { 162, new Instance("할라탈리 수련장", 1, 1, 2) },
            { 158, new Instance("브레이플록스의 야영지", 1, 1, 2) },
            { 163, new Instance("카른의 무너진 사원", 1, 1, 2) },
            { 159, new Instance("방랑자의 궁전", 1, 1, 2) },
            { 168, new Instance("돌방패 경계초소", 1, 1, 2) },
            { 170, new Instance("나무꾼의 비명", 1, 1, 2) },
            { 171, new Instance("제멜 요새", 1, 1, 2) },
            { 167, new Instance("옛 암다포르 성", 1, 1, 2) },
            { 217, new Instance("카스트룸 메리디아눔", 2, 2, 4) },
            { 224, new Instance("마도성 프라이토리움", 2, 2, 4) },
            { 160, new Instance("시리우스 대등대", 1, 1, 2) },
            { 349, new Instance("구리종 광산(어려움)", 1, 1, 2) },
            { 350, new Instance("하우케타 별궁(어려움)", 1, 1, 2) },
            { 362, new Instance("브레이플록스의 야영지(어려움)", 1, 1, 2) },
            { 360, new Instance("할라탈리 수련장(어려움)", 1, 1, 2) },
            { 363, new Instance("옛 암다포르 시가지", 1, 1, 2) },
            { 361, new Instance("난파선의 섬", 1, 1, 2) },
            { 373, new Instance("탐타라 묘소(어려움)", 1, 1, 2) },
            { 365, new Instance("돌방패 경계초소(어려움)", 1, 1, 2) },
            { 367, new Instance("카른의 무너진 사원(어려움)", 1, 1, 2) },
            { 371, new Instance("얼음외투 대빙벽", 1, 1, 2) },
            { 387, new Instance("사스타샤 침식 동굴(어려움)", 1, 1, 2) },
            { 189, new Instance("옛 암다포르 성(어려움)", 1, 1, 2) },
            { 188, new Instance("방랑자의 궁전(어려움)", 1, 1, 2) },

            // 던전 (3.x)
            { 416, new Instance("구브라 환상도서관", 1, 1, 2) },
            { 150, new Instance("묵약의 탑", 1, 1, 2) },
            { 420, new Instance("거두지 않는 섬", 1, 1, 2) },
            { 421, new Instance("이슈가르드 교황청", 1, 1, 2) },
            { 430, new Instance("무한연속 박물함", 1, 1, 2) },
            { 434, new Instance("어스름 요새", 1, 1, 2) },
            { 441, new Instance("솜 알", 1, 1, 2) },
            { 438, new Instance("마과학 연구소", 1, 1, 2) },
            { 435, new Instance("용의 둥지", 1, 1, 2) },


            // 길드 작전 (2.x)
            { 214, new Instance("집단전 훈련을 완수하라!", 1, 1, 2) },
            { 190, new Instance("방황하는 사령을 쓰러뜨려라!", 1, 1, 2) },
            { 215, new Instance("관문을 돌파하고 최심부의 적을 쓰러뜨려라!", 1, 1, 2) },
            { 216, new Instance("길거북을 사로잡아라!", 1, 1, 2) },
            { 191, new Instance("독성 요괴꽃을 제거하라!", 1, 1, 2) },
            { 192, new Instance("무법자 '나나니단'을 섬멸하라!", 1, 1, 2) },
            { 220, new Instance("몽환의 브라크시오를 쓰러뜨려라!", 1, 1, 2) },
            { 219, new Instance("폭탄광 고블린 군단을 섬멸하라!", 1, 1, 2) },
            { 221, new Instance("오염원 몰볼을 쓰러뜨려라!", 1, 1, 2) },
            { 222, new Instance("갱도에 나타난 요마 부소를 쓰러뜨려라!", 1, 1, 2) },
            { 223, new Instance("무적의 부하를 조종하는 요마를 쓰러뜨려라!", 1, 1, 2) },
            { 298, new Instance("봄을 거느린 '봄 여왕'을 쓰러뜨려라!", 1, 1, 2) },
            { 299, new Instance("불길한 진형을 짜는 요마를 섬멸하라!", 1, 1, 2) },
            { 300, new Instance("세 거인족을 제압하여 유물을 지켜내라!", 2, 2, 4) },


            // 토벌전 (2.x)
            { 202, new Instance("이프리트 토벌전", 1, 1, 2) },
            { 206, new Instance("타이탄 토벌전", 1, 1, 2) },
            { 208, new Instance("가루다 토벌전", 1, 1, 2) },
            { 292, new Instance("진 이프리트 토벌전", 2, 2, 4) },
            { 293, new Instance("진 타이탄 토벌전", 2, 2, 4) },
            { 294, new Instance("진 가루다 토벌전", 2, 2, 4) },
            { 332, new Instance("리트아틴 강습전", 2, 2, 4) },
            { 295, new Instance("극 이프리트 토벌전", 2, 2, 4) },
            { 296, new Instance("극 타이탄 토벌전", 2, 2, 4) },
            { 297, new Instance("극 가루다 토벌전", 2, 2, 4) },
            { 207, new Instance("선왕 모그루 모그 XII세 토벌전", 2, 2, 4) },
            { 364, new Instance("극왕 모그루 모그 XII세 토벌전", 2, 2, 4) },
            { 348, new Instance("알테마 웨폰 파괴작전", 2, 2, 4) },
            { 509, new Instance("이벤트용 임무: 3", 2, 2, 4) },
            { 353, new Instance("이벤트용 임무: 1", 2, 2, 4) },
            { 354, new Instance("이벤트용 임무: 2", 2, 2, 4) },
            { 281, new Instance("진 리바이어선 토벌전", 2, 2, 4) },
            { 359, new Instance("극 리바이어선 토벌전", 2, 2, 4) },
            { 368, new Instance("도름 키마이라 토벌전", 2, 2, 4) },
            { 369, new Instance("하이드라 토벌전", 2, 2, 4) },
            { 366, new Instance("길가메시 토벌전", 2, 2, 4) },
            { 374, new Instance("진 라무 토벌전", 2, 2, 4) },
            { 375, new Instance("극 라무 토벌전", 2, 2, 4) },
            { 377, new Instance("진 시바 토벌전", 2, 2, 4) },
            { 378, new Instance("극 시바 토벌전", 2, 2, 4) },
            { 142, new Instance("아마지나배 투기대회 결승전", 2, 2, 4) },
            { 394, new Instance("투신 오딘 토벌전", 2, 2, 4) },
            { 143, new Instance("성도 이슈가르드 방어전", 2, 2, 4) },
            { 426, new Instance("아씨엔 나브리알레스 토벌전", 2, 2, 4) },
            { 396, new Instance("진 길가메시 토벌전", 2, 2, 4) },

            // 토벌전 (3.x)
            { 432, new Instance("진 라바나 토벌전", 2, 2, 4) },
            { 446, new Instance("극 라바나 토벌전", 2, 2, 4) },
            { 436, new Instance("진 비스마르크 토벌전", 2, 2, 4) },
            { 447, new Instance("극 비스마르크 토벌전", 2, 2, 4) },
            { 437, new Instance("나이츠 오브 라운드 토벌전", 2, 2, 4) },


            // 레이드 (2.x)
            { 174, new Instance("크리스탈 타워: 고대인의 미궁", 6, 6, 12) },
            { 241, new Instance("대미궁 바하무트: 해후편 1", 2, 2, 4) },
            { 242, new Instance("대미궁 바하무트: 해후편 2", 2, 2, 4) },
            { 243, new Instance("대미궁 바하무트: 해후편 3", 2, 2, 4) },
            { 244, new Instance("대미궁 바하무트: 해후편 4", 2, 2, 4) },
            { 245, new Instance("대미궁 바하무트: 해후편 5", 2, 2, 4) },
            { 355, new Instance("대미궁 바하무트: 침공편 1", 2, 2, 4) },
            { 356, new Instance("대미궁 바하무트: 침공편 2", 2, 2, 4) },
            { 357, new Instance("대미궁 바하무트: 침공편 3", 2, 2, 4) },
            { 358, new Instance("대미궁 바하무트: 침공편 4", 2, 2, 4) },
            { 372, new Instance("크리스탈 타워: 시르쿠스 탑", 3, 6, 15) },
            { 380, new Instance("대미궁 바하무트: 침공편(영웅) 1", 2, 2, 4) },
            { 381, new Instance("대미궁 바하무트: 침공편(영웅) 2", 2, 2, 4) },
            { 382, new Instance("대미궁 바하무트: 침공편(영웅) 3", 2, 2, 4) },
            { 383, new Instance("대미궁 바하무트: 침공편(영웅) 4", 2, 2, 4) },
            { 193, new Instance("대미궁 바하무트: 진성편 1", 2, 2, 4) },
            { 194, new Instance("대미궁 바하무트: 진성편 2", 2, 2, 4) },
            { 195, new Instance("대미궁 바하무트: 진성편 3", 2, 2, 4) },
            { 196, new Instance("대미궁 바하무트: 진성편 4", 2, 2, 4) },
            { 151, new Instance("크리스탈 타워: 어둠의 세계", 3, 6, 15) },


            // PVP (2.x)
            { 175, new Instance("늑대우리", 0, 0, 0) },
            { 336, new Instance("늑대우리", 0, 0, 0) },
            { 337, new Instance("늑대우리 (매칭 파티 전용)", 0, 0, 0) },
            { 352, new Instance("늑대우리 (고정 소규모 파티 전용)", 0, 0, 0) },
            { 184, new Instance("늑대우리 (매칭 파티 전용)", 0, 0, 0) },
            { 186, new Instance("늑대우리 (고정 소규모 파티 전용)", 0, 0, 0) },
            { 376, new Instance("외곽 유적지대(제압전)", 0, 0, 0) },
            { 149, new Instance("★ 카르테노 평원: 외곽 유적지대", 0, 0, 0) },
            { 422, new Instance("외곽 유적지대(섬멸전)", 0, 0, 0) },
        };

        public static Instance GetInstance(ushort code)
        {
            if (instanceDict.ContainsKey(code))
            {
                return instanceDict[code];
            }
            return instanceDict[0];
        }
    }
}

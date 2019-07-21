---
layout: page
title: {{ site.name }}
---

# 임무/돌발 찾기 도우미 (DFAssist)

**DFAssist** 는 파이널 판타지 14의 임무 찾기 및 돌발 (Duty Finder / F.A.T.E.) 기능을 보조할 수 있는 소프트웨어입니다.
매칭을 돌려놓고 다른 일을 하다가 매칭 된 줄도 모르고 계속 다른 일을 하느라 확인을 못 누르는 사태를 방지하기 위해 만들었습니다.

## 정보

- 최신 버전 ```v20190721.1``` ([다운로드](https://github.com/jaehyuk-lee/DFAssist/releases/latest))
- 타겟 버전 ```Microsoft .NET Framework 4.0``` ([다운로드](https://www.microsoft.com/ko-kr/download/details.aspx?id=17851))
- 게임 버전 ```파이널 판타지 14 한국판, 버전 4.5```또는 ```글로벌판, 버전 5.0```

## 사용법

1. 상단 다운로드 링크의 파일 목록에서 ``DFAssist.v********.*.zip`` 파일을 내려받습니다.
2. 원하는 경로에 압축을 해제합니다.
3. 해당 경로에서 ``DFAssist.exe`` 파일을 실행합니다.
4. 파이널 판타지 14 클라이언트의 패킷을 읽기 위해 관리자 권한이 필요합니다.
5. 실행이 되지 않을 시 상단 ``Microsoft .NET Framework 4.0`` 다운로드 링크에서 파일을 받아 설치합니다.

## 기능

- 실시간 임무/돌발 찾기 상태를 볼 수 있는 전용 오버레이 UI
  - 파이널 판타지 14가 최소화되어 있는 상태에서 현재 매칭 상태를 실시간으로 확인 가능 (오버레이 & 아이콘 깜빡임)
  - 무작위 임무가 매칭됐을 시 매칭된 임무가 어떤 임무인지 입장 전 확인 가능 (설정 필요)
  - 미리 설정한 돌발 임무가 현재 위치한 맵에 발생했을 경우 알림 가능 (이벤트 돌발, 고대무기 돌발 등)
  - 매칭된 임무에 해당하는 매크로 자동 복사 기능 (매크로 데이터가 임무/돌발 데이터에 포함되어 있어야 합니다.)
  - 알림음 사용자 지정 가능
- 텔레그램이나 디스코드로 알림 기능
  - 디스코드 알림은 [DFAssist 디스코드 서버](https://discord.gg/RqesxtS)에 참여한 상태에만 알림 수신 가능
- 사용자가 원하는 서버로 임무 매칭 완료 또는 돌발임무 발생 POST 요청 가능 (추가적인 업데이트가 계획되어 있습니다.)

## 문제 해결

자주 발생하는 문제들은 [FAQ](https://jaehyuk-lee.github.io/DFAssist/faq)에서 확인해볼 수 있습니다.  
그 외에 다른 문제 발생시 [이슈 트래커](https://github.com/jaehyuk-lee/DFAssist/issues)에 발생 상황과 결과를 남겨주세요.  
프로그램 창의 로그 기록도 문제 해결에 큰 도움이 됩니다.

## json 파일 변환 도구

- [FFXIV_DATA_Conversion_Tool](https://github.com/Jaehyuk-Lee/FFXIV_DATA_Conversion_Tool/releases)

사용하기 쉽게 만들지는 않았습니다. 이건 쓰실 분만 쓰세요...  
파판 데이터 csv파일을 DFAssist용 json 파일로 변환해주는 프로그램입니다.

## 스크린샷

### 오버레이 UI

![오버레이 UI 1](https://i.imgur.com/US7qpwX.png)  
![오버레이 UI 2](https://i.imgur.com/Dd8xCqh.png)

### 임무 찾기 상황

![임무 찾기 상황](https://i.imgur.com/VNWOUyh.png)

### 임무 찾기 매칭됨

![임무 찾기 매칭됨](https://i.imgur.com/GeU5i23.gif)

### 돌발 발생함

![돌발 발생함](https://i.imgur.com/d2c2nc1.gif)

### 아이콘 깜빡임

![아이콘 깜빡임](https://i.imgur.com/ndNAFZ8.gif)

### 매칭된 무작위 임무 미리보기

![매칭된 무작위 임무 미리보기](https://i.imgur.com/Up0iXSM.png)

### 프로그램 UI

![프로그램 UI 1](https://i.imgur.com/u27a3Hh.png)  
![프로그램 UI 2](https://i.imgur.com/bkBCc2q.png)  
![프로그램 UI 3](https://i.imgur.com/9v7REPc.png)

### 알림창 아이콘

![알림창 아이콘](https://i.imgur.com/1zDkoDS.png)

## 저작권

```
  기재되어있는 회사명 · 제품명 · 시스템 이름은 해당 소유자의 상표 또는 등록 상표입니다.

  (C) 2010 - 2019 SQUARE ENIX CO., LTD All Rights Reserved. Korea Published by ACTOZ SOFT.
```

상단 회사에 저작권이 없는 코드, 리소스, 데이터 등은 모두
[퍼블릭 도메인](https://ko.wikipedia.org/wiki/%ED%8D%BC%EB%B8%94%EB%A6%AD_%EB%8F%84%EB%A9%94%EC%9D%B8)에 따라 배포됩니다.

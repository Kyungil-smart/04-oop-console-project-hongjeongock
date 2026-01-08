## 목표
- Town -> Dungeon -> Battle 흐름 완성
- 몬스터 3마리 처치 시 게임 클리어

---

## 추가/수정 파일 목록

### 새로 생성
- Scenes/DungeonScene.cs  
  - TownScene 구조 복사하여 이동 및 출력 구현  
  - 몬스터 배치 및 조우 처리  
  - 몬스터 접촉 시 BattleScene 전환  

- Scenes/BattleScene.cs  
  - 전투 UI 출력  
  - 공격, 아이템, 도망 선택 처리  
  - 턴 처리 로직 구현  
  - 전투 종료 후 씬 복귀  

- GameObjects/Monster.cs  
  - HP, ATK 보유  
  - 사망 여부 관리  

- Scenes/EndScene.cs  
  - 클리어 및 게임오버 출력  

---

## 기존 파일 수정 위치

### Managers/GameManager.cs
- DungeonScene 등록  
  - AddScene("Dungeon", new DungeonScene(_player));
- BattleScene는 상황에 따라 동적 생성 권장

---

### Scenes/TownScene.cs
- 던전 진입 입력 추가  
  - D 키 입력 시 SceneManager.Change("Dungeon")
- 안내 메시지 출력 추가  

---

### GameObjects/PlayerCharacter.cs
- 스탯 필드 추가  
  - MaxHP, HP, ATK, KillCount
- 이동 시 맵 범위 체크 추가  
- 전투 결과 반영  
  - 승리 시 KillCount 증가  
  - 사망 시 게임오버 처리  

---

### GameObjects/Potion.cs
- Use 기능 구현  
  - HP 회복  
  - MaxHP 초과 방지  

---

## DungeonScene 구현 순서

1. TownScene 맵 출력 로직 복사  
2. 던전 맵 생성  
3. 몬스터 좌표 고정 배치  
4. 플레이어 이동 처리  
5. 몬스터 타일 진입 시 BattleScene 전환  
   - 전달 데이터  
     - PlayerCharacter  
     - Monster  
     - 복귀 씬 키  

옵션
- B 키 입력 시 Town 복귀

---

## BattleScene 구현 순서

1. 상태 출력  
   - 플레이어 HP / MaxHP  
   - 몬스터 HP  

2. 메뉴 출력  
   - 1 공격  
   - 2 아이템  
   - 3 도망  

3. 입력 처리  
   - 공격: 데미지 계산  
   - 아이템: 포션 사용  
   - 도망: 확률 처리  

4. 몬스터 턴 처리  
   - 플레이어 HP 감소  

5. 종료 조건  
   - 몬스터 HP <= 0  
     - KillCount 증가  
     - 던전 복귀  
     - 몬스터 제거  
   - 플레이어 HP <= 0  
     - 게임오버 처리  

---

## 데이터 전달 방식

- BattleScene 생성자에서 전달  
  - PlayerCharacter player  
  - Monster monster  
  - string returnSceneKey  

- 승리 시 monster.IsDead = true 설정  
- DungeonScene 복귀 시 IsDead 몬스터 제거 처리  

---

## 완료 체크리스트

필수
- Town에서 던전 진입 가능  
- 던전에서 몬스터 조우 가능  
- 전투 진입 및 종료 정상 동작  
- 포션 사용 가능  
- 몬스터 3마리 처치 시 엔딩  

안정화
- 이동 시 배열 인덱스 오류 없음  
- 전투 종료 후 정상 복귀  
- 처치한 몬스터 재등장 없음  

# 콘솔 RPG 프로젝트 진행

## 목표
- Town → Dungeon → Battle 흐름 완성
- 몬스터 3마리 처치 시 게임 클리어
- 플레이어 사망 시 Game Over 처리

---

## 전체 흐름
1. TownScene
   - 던전 진입(예: `D`)
2. DungeonScene
   - 던전 맵 출력 + 플레이어 이동
   - 몬스터 배치
   - 몬스터와 조우 시 BattleScene으로 전환
3. BattleScene
   - 턴제 전투(공격/아이템/도망)
   - 전투 종료 후 DungeonScene으로 복귀
4. EndScene
   - `KillCount >= 3` 이면 클리어 엔딩 출력
   - 플레이어 사망이면 게임오버 출력

---

## 새로 생성

### DungeonScene.cs
- TownScene 구조를 복사해 던전용으로 수정
- 던전 맵 생성 및 출력
- 몬스터 좌표 배치
- 플레이어 이동 처리
- 몬스터 타일 진입 시 BattleScene 전환
- `B` 키로 Town 복귀

### BattleScene.cs
- 전투 UI 출력
  - 플레이어 HP / MaxHP
  - 몬스터 HP
- 메뉴 출력 및 입력 처리
  - 1: 공격
  - 2: 아이템(포션)
  - 3: 도망
- 턴 처리 로직(플레이어 턴 → 몬스터 턴)
- 종료 조건 처리
  - 몬스터 사망: KillCount 증가 + 던전 복귀 + 몬스터 제거 처리
  - 플레이어 사망: 게임오버 처리

### Monster.cs
- 몬스터 스탯 보유(HP, ATK)
- 사망 여부 관리
- 몬스터 이름
- 몬스처 처치시 골드 획득

### EndScene.cs
- 클리어/게임오버 메시지 출력
- 재시작/종료 선택

---

### 수정

#### `GameManager.cs`
- `DungeonScene`, `BattleScene`, `EndScene` 등록
- 최초 진입 씬 설정(Title 또는 Town)

### TownScene.cs
- 던전 진입 입력 추가
- 안내 메시지 출력 추가(조작키 안내 포함)

### PlayerCharacter.cs
- 스탯 필드 추가
  - `MaxHP`, `HP`, `ATK`, `KillCount`
- 이동 시 맵 범위 체크(인덱스 오류 방지)
- 전투 결과 반영
  - 승리 시 `KillCount` 증가
  - 사망 시 게임오버 처리 트리거

### Potion.cs
- `Use()` 기능 구현
  - HP 회복
  - `MaxHP` 초과 방지

---

## DungeonScene 구현 계획

1. TownScene의 맵 출력 로직 복사
2. 던전 맵(Tile[,]) 생성/초기화
3. 몬스터 좌표 배치
4. 플레이어 Enter 시
   - Field 연결
   - 시작 위치 설정
   - 시작 타일에 플레이어 배치
5. 플레이어 이동 중 몬스터 타일 조우 시
   - BattleScene으로 전환
   - 전달 데이터:
     - `PlayerCharacter player`
     - `Monster monster`
     - `string returnSceneKey`
6. Battle 종료 후 복귀 시
   - 처치된 몬스터(`IsDead`)는 타일에서 제거
   - 재등장 방지

옵션
- `B` 키 입력 시 Town 복귀

---

## BattleScene 구현 계획

1. 상태 출력
   - 플레이어 HP / MaxHP
   - 몬스터 HP
2. 메뉴 출력
   - 1 공격
   - 2 아이템(포션)
   - 3 도망
3. 입력 처리
   - 공격: 데미지 계산 및 몬스터 HP 감소
   - 아이템: 포션 사용(HP 회복)
   - 도망: 확률 처리(성공 시 복귀, 실패 시 몬스터 턴)
4. 몬스터 턴 처리
   - 플레이어 HP 감소
5. 종료 조건
   - 몬스터 HP <= 0
     - `KillCount` 증가
     - `monster.IsDead = true`
     - 던전 복귀
   - 플레이어 HP <= 0
     - 게임오버 처리(EndScene 전환 또는 GameOver 플래그)

---

## 데이터 전달 방식(전투 진입/복귀)

### 권장 방식
- `BattleScene` 생성자에서 데이터 전달
  - `PlayerCharacter player`
  - `Monster monster`
  - `string returnSceneKey`

### 전투 결과 처리
- 승리 시 `monster.IsDead = true`
- 던전 복귀 후 DungeonScene에서
  - `IsDead` 몬스터는 타일에서 제거
  - 같은 좌표에서 재조우되지 않게 처리

---

## 클리어 조건
- `player.KillCount >= 3` 이면 클리어
- 클리어 발생 시 `EndScene`으로 전환

---

## 체크리스트

### 필수
- Town에서 던전 진입 가능
- 던전에서 몬스터 조우 가능
- 전투 진입 및 종료 정상 동작
- 포션 사용 가능
- 몬스터 3마리 처치 시 엔딩

### 마무리?
- 이동 시 배열 인덱스 오류 없음
-  전투 종료 후 정상 복귀
-  처치한 몬스터 재등장 없음
-  입력 중복(키 눌림 유지)으로 씬 전환 반복 없음
 - 플레이어 및 몬스터 등 이모션 출력

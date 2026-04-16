# Switchfire

Unity 2022.3.17f1 기반 3D 탑다운 + 1인칭 전환 슈터 게임 프로토타입.

---

## 조작 방법

| 키 | 동작 |
|---|---|
| `WASD` | 이동 |
| `Left Shift` | 빠르게 이동 (속도 1.8배) |
| `마우스 이동` | 조준 (탑다운: 커서 방향 / 1인칭: 시점 회전) |
| `좌클릭` | 발사 |
| `F` | 탑다운 ↔ 1인칭 뷰 전환 |

---

## 구현된 시스템

### PlayerController
`Assets/01.Scripts/Player/PlayerController.cs`

Rigidbody 기반 물리 이동 컨트롤러.

- **탑다운 모드**: WASD가 월드 좌표계 기준으로 이동 (W=+Z, A=-X, S=-Z, D=+X). 플레이어는 마우스 커서 방향을 항상 바라봄.
- **1인칭 모드**: 플레이어 transform의 forward/right 기준으로 이동. 바라보는 방향이 앞 방향.
- **Shift 스프린트**: `currentSpeed` 임시 변수로 계산하여 `speed` 원본값을 유지. 매 프레임 누적되던 버그 수정.

```
speed (기본값) → currentSpeed = speed * 1.8 (Shift 시)
```

---

### TopDownAimer
`Assets/01.Scripts/Player/TopDownAimer.cs`

탑다운 모드에서 마우스 커서를 향해 플레이어를 회전시키는 컴포넌트.

- `Camera.main`을 Awake에서 자동 참조 (Inspector 연결 불필요)
- 마우스 위치 → 월드 좌표 Ray cast → 지면 평면과의 교점으로 방향 계산
- `aimPivot`: 무기/총구 방향을 부드럽게 Slerp 회전 (Update)
- `Rigidbody.MoveRotation`: 플레이어 몸통 회전 (FixedUpdate)
- `targetRot` 초기값 `Quaternion.identity` — 씬 로드 직후 zero quaternion 에러 방지

---

### FPSLook
`Assets/01.Scripts/Player/FPSLook.cs`

1인칭 모드 마우스 시점 제어.

- 마우스 X축: Rigidbody 좌우 회전 (`MoveRotation`)
- 마우스 Y축: FPS 카메라 오브젝트 상하 회전 (±80° 클램프)
- 커서 잠금 상태일 때만 동작

---

### ViewSwitcher
`Assets/01.Scripts/Camera/ViewSwitcher.cs`

`F` 키로 탑다운 ↔ 1인칭 뷰를 전환하는 컴포넌트.

| | 탑다운 | 1인칭 |
|---|---|---|
| 카메라 | TopDownCamera ON | FPSCamera ON |
| 커서 | 잠금 해제 | 잠금 |
| 플레이어 메시 | 표시 | 숨김 |
| TopDownAimer | 활성 | 비활성 |

---

### 카메라 시스템 (탑다운)

#### Cinemachine Virtual Camera
`Packages: com.unity.cinemachine 2.9.7`

- **Virtual Camera**: CameraTarget 오브젝트를 Follow 타겟으로 설정. Body = Transposer.
- **CinemachineBrain**: TopDownCamera(MainCamera)에 부착. Virtual Camera의 출력을 실제 카메라에 적용.

#### CameraTarget
`Assets/01.Scripts/Camera/CameraTarget.cs`

플레이어 위치를 LateUpdate로 추적하는 단순 래퍼 오브젝트. Cinemachine이 직접 Rigidbody 오브젝트를 추적할 때 발생하는 떨림을 방지하기 위해 분리.

#### TopDownFollow
`Assets/01.Scripts/Camera/TopDownFollow.cs`

Cinemachine을 보조하는 속도 기반 팔로우 스크립트 (TopDownCamera에 부착).

- 플레이어와의 XZ 거리 비율로 카메라 추적 속도 보간
- `distance / catchUpDistance` 비율 → `playerSpeed ~ maxCameraSpeed` 사이 Lerp
- Overshoot 방지 처리 포함
- Y축(높이)는 고정

```
가까울수록 → 플레이어 속도에 맞춤
멀어질수록 → maxCameraSpeed로 빠르게 따라붙음
```

---

### 무기 시스템

#### Shooter
`Assets/01.Scripts/Weapon/Shooter.cs`

뷰 모드에 따라 발사 방식을 분기하는 컨트롤러.

- **탑다운**: `ProjectileGun` — 발사체(Projectile) 생성 방식
- **1인칭**: `HitscanGun` — 레이캐스트 즉시 판정 방식

#### WeaponAimSync
`Assets/01.Scripts/Weapon/WeaponAimSync.cs`

무기 오브젝트의 방향을 조준 방향과 동기화.

---

## 씬 구조 (SampleScene)

```
SampleScene
├── Player
│   ├── PlayerController
│   ├── TopDownAimer
│   ├── FPSLook
│   ├── Shooter
│   └── WeaponPivot
│       └── Gun
├── TopDownCamera (MainCamera)
│   ├── CinemachineBrain
│   └── TopDownFollow
├── Virtual Camera (Cinemachine)
│   └── Follow → CameraTarget
├── CameraTarget
│   └── CameraTarget.cs → player 추적
└── FPSCamera
```

---

## 패키지

| 패키지 | 버전 | 용도 |
|---|---|---|
| `com.unity.cinemachine` | 2.9.7 | 탑다운 카메라 |
| `com.unity.textmeshpro` | 3.0.6 | 텍스트 렌더링 |
| `com.unity.timeline` | 1.7.6 | 시네마틱 시퀀스 |
| `com.unity.ugui` | 1.0.0 | UI 시스템 |
| `com.unity.visualscripting` | 1.9.1 | 비주얼 스크립팅 |
| `com.unity.collab-proxy` | 2.12.4 | Unity Version Control |

---

## 프로젝트 열기

```sh
# macOS Unity Hub에서 이 디렉토리를 프로젝트로 추가
# 또는 CLI:
/Applications/Unity/Hub/Editor/2022.3.17f1/Unity.app/Contents/MacOS/Unity \
  -projectPath /Users/vdlrwnsv/Desktop/development/Unity/Game/Switchfire
```

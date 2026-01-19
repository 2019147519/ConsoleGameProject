# Meta.txt 작성 규칙
 - 공백과 개행으로 구분
 - line 1: Total_number_of_stages 숫자
 - line 2~: 맵 파일 이름 순서대로 한줄씩

# 맵 파일 형식
 - 모든 문자는 정수로 입력
 - 공백과 개행으로 구분
 - line 1: 가로길이 세로길이
 - line 2: 시작x좌표 시작y좌표
 - line 3~: 맵 2차원 matrix
 - 0: ground
 - 1: button
 - 2: stair
 - 3: door (open)
 - 4: wall
 - 5: door (close)
 - 6: box
 - 7: box on button

 # narative.txt 작성 규칙
  - 스테이지 진입 시 상단에 출력할 텍스트를 입력한 후, <###>를 넣어주면 게임에서 텍스트를 출력해 준다
  - <###>는 한 줄에 단독으로 작성해야 한다
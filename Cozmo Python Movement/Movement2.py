import cozmo
import socket
import errno
from socket import error as socket_error

def Movement2(robot: cozmo.robot.Robot):
    try:
        s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    except socket_error as msg:
        robot.say_text("socket failed" + msg).wait_for_completed()
    ip = "10.0.1.10"
    port = 5000

    try:
        s.connect((ip, port))
    except socket_error as msg:
        robot.say_text("socket failed to bind").wait_for_completed()
    cont = True

    robot.say_text("ready").wait_for_completed()
    movement = {'w', 's'}
    turning = {'a', 'd'}
    while cont:
        bytedata = s.recv(4048)
        # data = str(bytedata)
        data = bytedata.decode('utf-8')
        if not data:
            cont = False
            s.close()
            quit()
        else:
            print(data)
            instructions = data.split(';')
            print(instructions)
            if instructions[0] == "stop":
                robot.cozmo.drive_wheels(0, 0, 0, 0)
            robot.cozmo.drive_wheels(100, 100, 10, 10)

    def handle_key(self, key_code, is_shift_down, is_ctrl_down, is_alt_down, is_key_down):
        '''
        Called
        on
        any
        key
        press or release
        Holding
        a
        key
        down
        may
        result in repeated
        handle_key
        calls
        with is_key_down == True

    '''

    # Update desired speed / fidelity of actions based on shift/alt being held
    was_go_fast = self.go_fast
    was_go_slow = self.go_slow

    self.go_fast = is_shift_down
    self.go_slow = is_alt_down

    speed_changed = (was_go_fast != self.go_fast) or (was_go_slow != self.go_slow)

    # Update state of driving intent from keyboard, and if anything changed then call update_driving
    update_driving = True
    if key_code == ord('W'):
        self.drive_forwards = is_key_down
    elif key_code == ord('S'):
        self.drive_back = is_key_down
    elif key_code == ord('A'):
        self.turn_left = is_key_down
    elif key_code == ord('D'):
        self.turn_right = is_key_down
    else:
        if not speed_changed:
            update_driving = False
cozmo.run_program(Movement2)

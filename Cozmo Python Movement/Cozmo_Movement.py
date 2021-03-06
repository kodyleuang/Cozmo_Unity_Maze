
# Cozmo Movement is based off a list in the form of [Name;FB;LR;distX;distY]
# Corresponding with [Name, Forward/Backward; Left/Right; Distance X; Distance Y]


import cozmo
import socket
import errno
from socket import error as socket_error

# need to get movement info
from cozmo.util import degrees, distance_mm, speed_mmps


def cozmo_program(robot: cozmo.robot.Robot):
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


    # keys cozmo responds to
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
            # check the name to Stop movement, move forward or back, or turn R/L:
            if instructions[0] == "stop":
                robot.stop_all_motors()
            if instructions[0] in movement:
                # Movement is based on F forwards or B backwards and the speed at which both wheels move
                if instructions[1] == 'F':
                    robot.drive_wheels(100, 100, 0, 0)
                elif instructions[1] == 'B':
                    robot.drive_wheels(-100, -100, 0, 0)

            # We want to turn left or right
            if instructions[0] in turning:
                if instructions[0] == 'a':
                    robot.turn_in_place(degrees(90)).wait_for_completed()
                elif instructions[0] == 'd':
                    robot.turn_in_place(degrees(-90)).wait_for_completed()


                    print(instructions)
cozmo.run_program(cozmo_program)

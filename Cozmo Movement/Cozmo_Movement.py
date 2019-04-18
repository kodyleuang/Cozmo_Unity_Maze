
# Cozmo Movement is based off a list in the form of [Name;FB;LR;distX;distY]
# Corresponding with [Name, Forward/Backward; Left/Right; Distance X; Distance Y]

#robot.stop_all_motors()

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

    # SET COZMO's NAME
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
                print("i was supposed to stop")
            if instructions[0] in movement:
                # we know that this is a message involving movement
                instructions[2] = int(instructions[2])
                # next, we will want to move forward or backward, if the x distance is not 0
                # first, just move if 'F' and move backwards for 'B'
                if instructions[2] != 0:
                    distX = instructions[2]
                    if instructions[1] == 'F':
                        robot.drive_straight(distance_mm(distX), speed_mmps(150))
                    elif instructions[1] == 'B':
                        robot.drive_straight(distance_mm(distX), speed_mmps(150))

            # We want to turn left or right
            if instructions[0] in turning:
                if instructions[0] == 'a':
                    robot.turn_in_place(degrees(90)).wait_for_completed()
                elif instructions[0] == 'd':
                    robot.turn_in_place(degrees(-90)).wait_for_completed()


                    print(instructions)
             #   elif len(instructions) == 3:
             #      # this is where we move the tractor or the head
             #       # if the first value is greater than 0, move the head
             #       # if the second value is greater than 0, move the tractor arm
             #       instructions[1] = float(instructions[1])
             #       instructions[2] = float(instructions[2])
             #       print(instructions)
             #       headAngle = instructions[1]
             #       headAngle = max(-25, min(headAngle, 44.5))

             #       robot.set_head_angle(degrees(headAngle)).wait_for_completed()

             #       liftArm = instructions[2]
             #       liftArm = max(0, min(liftArm, 1.0))

             #       robot.set_lift_height(liftArm, in_parallel=True).wait_for_completed()


cozmo.run_program(cozmo_program)

class GestureMapper:
    def __init__(self):
        pass

    def map_gesture(self, gesture):
        """
        Convert classified gesture into a driving command.
        """

        if gesture == "FULL_SPEED":
            return "ACCELERATE_MAX"

        if gesture == "SLOW_SPEED":
            return "ACCELERATE_HALF"

        if gesture == "REVERSE":
            return "REVERSE"

        if gesture == "BRAKE":
            return "BRAKE"

        if gesture == "LEFT":
            return "TURN_LEFT"

        if gesture == "RIGHT":
            return "TURN_RIGHT"

        return "IDLE"

class GestureMapper:
    def __init__(self):
        pass

    def map_gesture(self, gesture):
        if gesture in [
            "FULL_SPEED",
            "REVERSE",
            "BRAKE",
            "LEFT",
            "RIGHT"
        ]:
            return gesture

        return "IDLE"

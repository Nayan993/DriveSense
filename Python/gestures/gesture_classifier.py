import math


class GestureClassifier:
    def __init__(self):
        pass

    def classify(self, landmarks):
        """
        Classify hand gesture based on landmarks.
        landmarks: list of (x, y, z), length = 21
        returns: gesture string or None
        """

        if landmarks is None or len(landmarks) != 21:
            return None

        # Finger tip indices in MediaPipe
        thumb_tip = 4
        index_tip = 8
        middle_tip = 12
        ring_tip = 16
        pinky_tip = 20

        # Finger base indices
        index_base = 5
        middle_base = 9
        ring_base = 13
        pinky_base = 17

        # Helper: check if finger is open
        def finger_open(tip, base):
            return landmarks[tip][1] < landmarks[base][1]

        index_open = finger_open(index_tip, index_base)
        middle_open = finger_open(middle_tip, middle_base)
        ring_open = finger_open(ring_tip, ring_base)
        pinky_open = finger_open(pinky_tip, pinky_base)

        open_fingers = sum([index_open, middle_open, ring_open, pinky_open])

        # Palm orientation (up/down)
        wrist_y = landmarks[0][1]
        middle_base_y = landmarks[middle_base][1]
        palm_down = wrist_y < middle_base_y

        # Hand tilt (left/right)
        wrist_x = landmarks[0][0]
        index_base_x = landmarks[index_base][0]
        tilt = index_base_x - wrist_x

        # Gesture rules (as decided)
        if open_fingers == 4 and not palm_down:
            return "FULL_SPEED"

        if open_fingers == 2 and index_open and middle_open:
            return "REVERSE"

        if open_fingers == 0:
            return "SLOW_SPEED"

        if palm_down:
            return "BRAKE"

        if tilt > 0.05:
            return "RIGHT"

        if tilt < -0.05:
            return "LEFT"

        return "IDLE"

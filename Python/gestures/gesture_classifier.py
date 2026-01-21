class GestureClassifier:
    def __init__(self):
        pass

    def classify(self, landmarks):
        if landmarks is None or len(landmarks) != 21:
            return None

        # Landmark indices
        wrist = 0
        thumb_tip = 4
        index_tip = 8
        middle_tip = 12
        ring_tip = 16
        pinky_tip = 20

        index_base = 5
        middle_base = 9
        ring_base = 13
        pinky_base = 17

        def finger_open(tip, base):
            return landmarks[tip][1] < landmarks[base][1]

        index_open = finger_open(index_tip, index_base)
        middle_open = finger_open(middle_tip, middle_base)
        ring_open = finger_open(ring_tip, ring_base)
        pinky_open = finger_open(pinky_tip, pinky_base)

        open_fingers = sum([index_open, middle_open, ring_open, pinky_open])

        # ---------------- BRAKE ----------------
        if open_fingers == 0:
            return "BRAKE"

        # ---------------- FULL SPEED ----------------
        if open_fingers == 4:
            return "FULL_SPEED"

        # ---------------- REVERSE ----------------
        if index_open and middle_open and not ring_open and not pinky_open:
            return "REVERSE"

        # ---------------- SLOW SPEED (Half-closed palm) ----------------
        if open_fingers == 3:
            return "SLOW_SPEED"

        # ---------------- LEFT ----------------
        if index_open and not middle_open and not ring_open and not pinky_open:
            return "LEFT"

        # ---------------- RIGHT ----------------
        if pinky_open and not index_open and not middle_open and not ring_open:
            return "RIGHT"

        return "IDLE"

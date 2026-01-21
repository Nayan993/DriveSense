import cv2
import mediapipe as mp


class HandDetector:
    def __init__(
        self,
        max_num_hands=1,
        detection_confidence=0.7,
        tracking_confidence=0.7,
    ):
        self.mp_hands = mp.solutions.hands
        self.mp_draw = mp.solutions.drawing_utils

        self.hands = self.mp_hands.Hands(
            static_image_mode=False,
            max_num_hands=max_num_hands,
            min_detection_confidence=detection_confidence,
            min_tracking_confidence=tracking_confidence,
        )

    def detect_hand(self, frame):
        rgb_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        results = self.hands.process(rgb_frame)

        annotated_frame = frame.copy()

        if not results.multi_hand_landmarks:
            return None, annotated_frame

        hand_landmarks = results.multi_hand_landmarks[0]

        self.mp_draw.draw_landmarks(
            annotated_frame,
            hand_landmarks,
            self.mp_hands.HAND_CONNECTIONS,
        )

        landmarks = [(lm.x, lm.y, lm.z) for lm in hand_landmarks.landmark]

        return landmarks, annotated_frame

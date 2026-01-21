import cv2

from camera.camera import Camera
from gestures.hand_detector import HandDetector
from gestures.gesture_classifier import GestureClassifier
from gestures.gesture_mapping import GestureMapper


def main():
    camera = Camera()
    detector = HandDetector()
    classifier = GestureClassifier()
    mapper = GestureMapper()

    while True:
        frame = camera.read_frame()
        if frame is None:
            break

        landmarks, annotated_frame = detector.detect_hand(frame)

        gesture = classifier.classify(landmarks)
        command = mapper.map_gesture(gesture)

        if gesture:
            cv2.putText(
                annotated_frame,
                f"Gesture: {gesture}",
                (20, 40),
                cv2.FONT_HERSHEY_SIMPLEX,
                1,
                (0, 255, 0),
                2,
            )

            cv2.putText(
                annotated_frame,
                f"Command: {command}",
                (20, 80),
                cv2.FONT_HERSHEY_SIMPLEX,
                1,
                (255, 0, 0),
                2,
            )

        cv2.imshow("DriveSense - Gesture Control", annotated_frame)

        if cv2.waitKey(1) & 0xFF == ord("q"):
            break

    camera.release()
    cv2.destroyAllWindows()


if __name__ == "__main__":
    main()

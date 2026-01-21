import cv2
import time


class Camera:
    def __init__(self, camera_id=0, width=640, height=480):
        # Force V4L2 backend for Linux stability
        self.cap = cv2.VideoCapture(camera_id, cv2.CAP_V4L2)

        self.cap.set(cv2.CAP_PROP_FRAME_WIDTH, width)
        self.cap.set(cv2.CAP_PROP_FRAME_HEIGHT, height)

        if not self.cap.isOpened():
            raise RuntimeError("Cannot open camera")

        # Warm-up frames (important on Linux)
        for _ in range(5):
            self.cap.read()
            time.sleep(0.05)

    def read_frame(self):
        ret, frame = self.cap.read()
        if not ret:
            return None
        return frame

    def release(self):
        if self.cap:
            self.cap.release()
import cv2
import time


class Camera:
    def __init__(self, width=640, height=480):
        self.cap = None

        # Try camera indexes safely
        for index in range(3):
            cap = cv2.VideoCapture(index, cv2.CAP_ANY)
            if cap.isOpened():
                self.cap = cap
                break

        if self.cap is None:
            raise RuntimeError("No usable camera found")

        self.cap.set(cv2.CAP_PROP_FRAME_WIDTH, width)
        self.cap.set(cv2.CAP_PROP_FRAME_HEIGHT, height)

        # Warm-up
        for _ in range(5):
            self.cap.read()
            time.sleep(0.05)

    def read_frame(self):
        ret, frame = self.cap.read()
        if not ret:
            return None
        return frame

    def release(self):
        if self.cap:
            self.cap.release()

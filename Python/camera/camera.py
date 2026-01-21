import cv2
import time


class Camera:
    def __init__(self, width=640, height=480):
        self.cap = None

        # Try combinations of backends and indexes
        backends = [
            cv2.CAP_ANY,
            cv2.CAP_V4L2,
            cv2.CAP_FFMPEG,
        ]

        for backend in backends:
            for index in range(3):
                cap = cv2.VideoCapture(index, backend)
                if cap.isOpened():
                    self.cap = cap
                    break
            if self.cap:
                break

        if self.cap is None:
            print("WARNING: Camera not opened yet, retrying...")
            self.cap = cv2.VideoCapture(0)

        self.cap.set(cv2.CAP_PROP_FRAME_WIDTH, width)
        self.cap.set(cv2.CAP_PROP_FRAME_HEIGHT, height)

        # Warm up camera
        for _ in range(10):
            self.cap.read()
            time.sleep(0.05)

    def read_frame(self):
        if not self.cap:
            return None

        ret, frame = self.cap.read()
        if not ret:
            return None

        return frame

    def release(self):
        if self.cap:
            self.cap.release()

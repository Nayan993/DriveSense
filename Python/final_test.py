import cv2
import time

def test_forced_mjpg():
    print("[INFO] Attempting to open Camera Index 0 with MJPG forcing...")

    # 1. Open Index 0 using V4L2 backend
    cap = cv2.VideoCapture(0, cv2.CAP_V4L2)

    # 2. CRITICAL: Force MJPG Format
    # This prevents the "Inappropriate ioctl" error caused by YUYV bandwidth overflow
    cap.set(cv2.CAP_PROP_FOURCC, cv2.VideoWriter_fourcc(*'MJPG'))

    # 3. Set standard resolution
    cap.set(cv2.CAP_PROP_FRAME_WIDTH, 640)
    cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 480)
    cap.set(cv2.CAP_PROP_FPS, 30)

    if not cap.isOpened():
        print("[ERROR] Failed to open camera.")
        return

    # Warm up
    time.sleep(1)

    # Try reading
    ret, frame = cap.read()
    if ret:
        print("[SUCCESS] Frame captured successfully!")
        print(f"Resolution: {frame.shape[1]}x{frame.shape[0]}")
        
        # Show the frame to prove it works
        cv2.imshow("Success!", frame)
        print("Press any key to close the window...")
        cv2.waitKey(0)
        cv2.destroyAllWindows()
    else:
        print("[ERROR] Camera opened but returned no frame.")

    cap.release()

if __name__ == "__main__":
    test_forced_mjpg()
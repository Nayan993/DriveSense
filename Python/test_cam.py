import cv2

def test_camera_one():
    print("Attempting to open camera at index 1 (/dev/video1)...")
    
    # Force V4L2 backend
    cap = cv2.VideoCapture(1, cv2.CAP_V4L2)
    
    # Optional: Force MJPG format (helps with some USB cameras)
    # cap.set(cv2.CAP_PROP_FOURCC, cv2.VideoWriter_fourcc(*"MJPG"))
    
    if not cap.isOpened():
        print("ERROR: Could not open /dev/video1. Check permissions or if another app is using it.")
        return

    print("Success: Camera opened. Attempting to read frame...")
    ret, frame = cap.read()
    
    if ret:
        print("SUCCESS! Frame captured.")
        cv2.imwrite("test_image.jpg", frame)
        print("Saved 'test_image.jpg' to current folder.")
    else:
        print("ERROR: Camera opened, but failed to grab a frame.")
    
    cap.release()

if __name__ == "__main__":
    test_camera_one()
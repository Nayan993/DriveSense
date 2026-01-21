import socket


class UnitySender:
    def __init__(self, ip="127.0.0.1", port=5055):
        self.server_address = (ip, port)
        self.sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

    def send(self, message: str):
        if not message:
            return
        data = message.encode("utf-8")
        self.sock.sendto(data, self.server_address)

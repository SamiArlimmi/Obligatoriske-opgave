import json
from socket import *
import threading

# Funktion til at håndtere en tilsluttet klient
def handleClient(connectionSocket, address):
    while True:
        try:
            data = connectionSocket.recv(1024)  # Modtag data fra klienten (op til 1024 bytes)
            if not data:
                break

            request = json.loads(data.decode())  # Decode den modtagne JSON-data til en Python-dictionary

            # Tjek om anmodningen har påkrævede felter
            if "method" not in request or "Tal1" not in request or "Tal2" not in request:
                response = {"error": "Invalid request format"}
            else:
                method = request["method"]
                num1 = request["Tal1"]
                num2 = request["Tal2"]
                result = 0

                # Udfør den anmodede beregning baseret på metoden
                if method == "+":
                    result = num1 + num2
                elif method == "-":
                    result = num1 - num2
                elif method == "*":
                    result = num1 * num2
                elif method == "/":
                    if num2 != 0:
                        result = num1 / num2
                    else:
                        response = {"error": "Division by zero"}
                        connectionSocket.send(json.dumps(response).encode())
                        continue

                response = {"result": result}

            connectionSocket.send(json.dumps(response).encode())  # Kode og send svaret som JSON

        except json.JSONDecodeError:
            response = {"error": "Invalid JSON format"}
            connectionSocket.send(json.dumps(response).encode())

        except Exception as e:
            response = {"error": str(e)}
            connectionSocket.send(json.dumps(response).encode())

    connectionSocket.close()  # Luk forbindelsen med klienten, når færdig


serverName = "127.0.0.1"  
serverPort = 12000   
serverSocket = socket(AF_INET, SOCK_STREAM)  
serverSocket.bind((serverName, serverPort)) 
serverSocket.listen(5)  
print('Server is ready to listen')

# Accepter og håndter indkommende forbindelser kontinuerligt
while True:
    connectionSocket, addr = serverSocket.accept()  # Accepter en indkommende forbindelse
    threading.Thread(target=handleClient, args=(connectionSocket, addr)).start()  # Start en ny thread til at håndtere klienten

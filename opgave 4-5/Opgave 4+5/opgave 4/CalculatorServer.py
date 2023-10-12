from socket import *
import threading

# Funktion til at håndtere en tilsluttet klient
def handleClient(connectionSocket, address):
    while True:
        data = connectionSocket.recv(1024)
        msg = data.decode()  
        msg = msg.strip().lower() 

        # Tjek om klienten ønsker at afslutte
        if msg == 'Exit':
            print("Connection terminated")
            connectionSocket.close()  
            break

        print("Received message from client:", msg)

        
        result = 0
        operation_list = msg.split()  
        oprnd1 = operation_list[0]  
        operation = operation_list[1]  
        oprnd2 = operation_list[2]  

        num1 = int(oprnd1)  # Konverter operanderne til et heltal
        num2 = int(oprnd2)  

        # Ønsket beregning
        if operation == "+":
            result = num1 + num2
        elif operation == "-":
            result = num1 - num2
        elif operation == "/":
            result = num1 / num2
        elif operation == "*":
            result = num1 * num2

        # Send resultatet tilbage til klienten
        output = str(result)
        connectionSocket.send(output.encode())  # Send resultatet til klienten

    connectionSocket.close()  # Luk forbindelsen med klienten når færdig

# Serverkonfiguration
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

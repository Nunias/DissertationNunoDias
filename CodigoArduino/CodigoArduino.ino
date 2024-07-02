//libraries
#include "Firebase_Arduino_WiFiNINA.h"    //DataBase
#include <WiFiNINA.h>
//#include <FirebaseClient.h>
#include <SPI.h>                          //Encoder

//pins
#define BUZZER_PIN 4                      //Peripherals
#define LED_PIN 5                         //Peripherals
#define AMT22_NOP 0x00                    //Encoder
#define AMT22_RESET 0x60                  //Encoder
#define AMT22_ZERO 0x70                   //Encoder
#define RES12 12                          //Encoder
#define RES14 14                          //Encoder
#define ENC_0 3                           //Encoder
#define SPI_MOSI MOSI                     //Encoder
#define SPI_MISO MISO                     //Encoder
#define SPI_SCLK SCK                      //Encoder

//wifi                                    //DataBase
//NetCasa-Nuno
//#define WIFI_SSID "MEO-2B42E5"
//#define WIFI_PASSWORD "C807A0DCD6"
//NetTelemovel
#define WIFI_SSID "Os meus dados"
#define WIFI_PASSWORD "nunodias123"

//Firebase                                //DataBase
#define FIREBASE_HOST "kneebrace2324-default-rtdb.europe-west1.firebasedatabase.app"
#define FIREBASE_AUTH "tJpdUJr2cwGoI49WtLEW76E43gqlwUSyCiELfLHl"

//Define Firebase data object
FirebaseData firebaseData;
String ComplitPath = "/Exercise2/angle";  //Path da Base de Dados. 1-Simular a andar

//Sounds
int GoodSound[] = { 510, 510, 0 };         // Notas: C4, E4, G4, C5
int GoodSoundNoteDurations[] = { 10, 1 };  // Durações: 4 tempos cada
int SizeGood = sizeof(GoodSound) / sizeof(GoodSound[0]);
//-------------------------------------
int BadSound[] = { 50, 0 };
int BadSoundNoteDurations[] = { 1, 1 };
int SizeBad = sizeof(GoodSound) / sizeof(GoodSound[0]);

//variaveis auxiliares
int j = 0;
int i = 0;
bool reconnect = false;

uint16_t encoderPosition;                //Encoder
float encoderPositionDegree;             //Encoder
uint8_t attempts;                        //Encoder

void setup() {
  //Peripherals
  pinMode(BUZZER_PIN, OUTPUT);
  pinMode(LED_PIN, OUTPUT);
  //Encoder
  pinMode(SPI_SCLK, OUTPUT);
  pinMode(SPI_MOSI, OUTPUT);
  pinMode(SPI_MISO, INPUT);
  pinMode(ENC_0, OUTPUT);

  Serial.begin(9600);
  //while (!Serial) {
  //  ;
  //}

  //Encoder
  digitalWrite(ENC_0, HIGH);
  SPI.begin();
  setZeroSPI(ENC_0);

  //Conecting to WIFI
  Serial.println("----------");
  Serial.print("Conectando ao Wi-Fi");
  int status = WL_IDLE_STATUS;
  while (status != WL_CONNECTED) {
    status = WiFi.begin(WIFI_SSID, WIFI_PASSWORD);
    Serial.print(".");
    digitalWrite(LED_PIN, HIGH); // Turn the LED on (HIGH is the voltage level)
    delay(300);                // Wait for a second
    digitalWrite(LED_PIN, LOW);  // Turn the LED off by making the voltage LOW

    //delay(300);
  }
  Serial.println();
  Serial.print("IP: ");
  Serial.println(WiFi.localIP());
  Serial.println("----------");
  //Connection to Firebase
  Firebase.begin(FIREBASE_HOST, FIREBASE_AUTH, WIFI_SSID, WIFI_PASSWORD);
  Firebase.reconnectWiFi(true);  //Let's say that if the connection is down, try to reconnect automatically

  if (!Firebase.beginStream(firebaseData, "/test/data")) {
    //Could not begin stream connection, then print out the error detail
    Serial.println(firebaseData.errorReason());
  }

  Serial.println("Bom");
  PlaySound(GoodSound, GoodSoundNoteDurations, SizeGood);  
  digitalWrite(LED_PIN, LOW);
}

void loop() {
  
  /*
  Serial.println("Bom");
  PlaySound(GoodSound, GoodSoundNoteDurations, SizeGood); // Tocar o som "Bom!"
  delay(500); // Esperar 1 segundo
  */

  /*
  Serial.println("Mau");
  PlaySound(BadSound, BadSoundNoteDurations, SizeBad); // Tocar o som "Mau!"
  delay(500); // Esperar 1 segundo
  */

  float n = readEncoderValues();
  if (Firebase.setFloat(firebaseData, ComplitPath, n)) {
    if (firebaseData.dataType() == "float")      // se hace para asegurar que si el tipo almacenado es entero cogemso el valor correctamente y no basura
      Serial.print(i);
      Serial.print(" - ");
      Serial.println(firebaseData.floatData());  //Realmente estamos leyendo con esto el valor introducido
    if(!(i%95)){
      Serial.println("Ja deu 90 valores");
      reconnectFirebase();
      reconnect=true;
    }
    else reconnect=false;
    i = i + 1;
  } else {
    Serial.println("ERROR : " + firebaseData.errorReason());
    Serial.print(i);
    Serial.println();
    i = 0;
    digitalWrite(LED_BUILTIN, HIGH);  // turn the LED on (HIGH is the voltage level)
    delay(1000);                      // wait for a second
    digitalWrite(LED_BUILTIN, LOW);   // turn the LED off by making the voltage LOW
  }
  if(!reconnect){
    delay(50);
  }
  
}

// Função para tocar um som
void PlaySound(int sound[], int duration[], int size) {
  for (int thisNote = 0; thisNote < size; thisNote++) {
    int noteDuration = 1000 / duration[thisNote];
    tone(BUZZER_PIN, sound[thisNote], noteDuration);
    int pauseBetweenNotes = noteDuration * 1.30;
    delay(pauseBetweenNotes);
    noTone(BUZZER_PIN);
    delay(50);  // Adiciona um pequeno atraso entre as notas
  }
}

//funcao para ler valores do encoder
float readEncoderValues() {
  attempts = 0;

  encoderPosition = getPositionSPI(ENC_0, RES14);

  while (encoderPosition == 0xFFFF && ++attempts < 3) {
    encoderPosition = getPositionSPI(ENC_0, RES14);
  }

  if (encoderPosition == 0xFFFF) {
    Serial.print("Encoder error. Attempts: ");
    Serial.print(attempts, DEC);
    //Serial.write(NEWLINE);
  } else {
    //Serial.print("Encoder: ");
    //Serial.print(encoderPosition, DEC);
    //Serial.print("   -   ");
    encoderPositionDegree = encoderPosition / 45.5083; //transforma em graus
    encoderPositionDegree=360-encoderPositionDegree;   //muda o sentido de rotação do encoder
    encoderPositionDegree=round(encoderPositionDegree * 100) / 100;  //arredonda para um float de 2 casas decimais
    if(encoderPositionDegree>180){
      encoderPositionDegree = encoderPositionDegree-360;
    }
    else 
      encoderPositionDegree = encoderPositionDegree;

    //Serial.println(encoderPositionDegree);
    return encoderPositionDegree;
    //Serial.write(NEWLINE);
  }
}

uint16_t getPositionSPI(uint8_t encoder, uint8_t resolution) {
  uint16_t currentPosition;
  bool binaryArray[16];

  currentPosition = spiWriteRead(AMT22_NOP, encoder, false) << 8;
  delayMicroseconds(3);
  currentPosition |= spiWriteRead(AMT22_NOP, encoder, true);

  for (int i = 0; i < 16; i++) {
    binaryArray[i] = (0x01) & (currentPosition >> (i));
  }

  if ((binaryArray[15] == !(binaryArray[13] ^ binaryArray[11] ^ binaryArray[9] ^ binaryArray[7] ^ binaryArray[5] ^ binaryArray[3] ^ binaryArray[1])) && (binaryArray[14] == !(binaryArray[12] ^ binaryArray[10] ^ binaryArray[8] ^ binaryArray[6] ^ binaryArray[4] ^ binaryArray[2] ^ binaryArray[0]))) {
    currentPosition &= 0x3FFF;
  } else {
    Serial.println("Encoder error.");
    currentPosition = 0xFFFF;
  }

  if ((resolution == RES12) && (currentPosition != 0xFFFF)) {
    currentPosition = currentPosition >> 2;
  }

  return currentPosition;
}

uint8_t spiWriteRead(uint8_t sendByte, uint8_t encoder, uint8_t releaseLine)
{
  //holder for the received over SPI
  uint8_t data;

  //set cs low, cs may already be low but there's no issue calling it again except for extra time
  setCSLine(encoder ,LOW);

  //There is a minimum time requirement after CS goes low before data can be clocked out of the encoder.
  //We will implement that time delay here, however the arduino is not the fastest device so the delay
  //is likely inherantly there already
  delayMicroseconds(3);

  //send the command  
  data = SPI.transfer(sendByte);
  delayMicroseconds(3); //There is also a minimum time after clocking that CS should remain asserted before we release it
  setCSLine(encoder, releaseLine); //if releaseLine is high set it high else it stays low
  
  return data;
}

void setCSLine (uint8_t encoder, uint8_t csLine)
{
  digitalWrite(encoder, csLine);
}

void setZeroSPI(uint8_t encoder)
{
  spiWriteRead(AMT22_NOP, encoder, false);

  //this is the time required between bytes as specified in the datasheet.
  //We will implement that time delay here, however the arduino is not the fastest device so the delay
  //is likely inherantly there already
  delayMicroseconds(3); 
  
  spiWriteRead(AMT22_ZERO, encoder, true);
  delay(250); //250 second delay to allow the encoder to reset
}

void reconnectFirebase() {
  // Fecha a conexão com o stream do Firebase
  Firebase.endStream(firebaseData);
  // Reconecta ao Firebase
  Firebase.begin(FIREBASE_HOST, FIREBASE_AUTH, WIFI_SSID, WIFI_PASSWORD);
}



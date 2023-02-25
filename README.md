![Immagine](https://user-images.githubusercontent.com/111366201/221373822-c1cfa48f-efd6-454a-9a6f-364395450bdc.png)

# Folder Locker
Folder Locker è un programmino che blocca e sblocca una singola cartella con una password

L'utilizzo di questo programma è molto semplice

### Configurazione prima dell'utilizzo

1) Apri il registro di Sistema
2) Naviga in HKEY_CURRENT_USER\Software e creare una nuova chiave con questo nome : ``` Folder Locker ```
3) Creare un valore stringa con nome ```DirPath``` e come valore inserire il percorso della cartella che conterrà un'altra cartella che verrà protetta dalla password

Il programma accetta solamente password criptate in Base64, per ottenere una password criptata in Base64 accedere a questa pagina per crearne una : https://www.base64encode.org/

4) Creare un altro valore stringa con nome ```Password```, inserire nel valore la password criptata creata in precedenza

### Bene dopo aver creato questi valori sul registro sarai pronto a utilizzare Folder Locker

Come utilizzarlo:

- Come Sbloccare la Cartella

Per sbloccare la cartella protetta da password, inserirla nel campo apposito e cliccare su Sblocca, il programma andrà a leggere il valore salvato sul registro è andra a decriptarla e leggerla e verificare se è corretta o no, se la password è corretta apparirà una cartella nuova nominata "Private", dentro questa cartella ci si può mettere tutto quello che si vuole (che si vuole proteggere).

- Come Bloccare la Cartella

Dopo aver sbloccato la cartella e si vuole ribloccarla nuovamente basta cliccare su Blocca, e il programma andrà a bloccare la cartella, dopo che la cartella verrà bloccata, sparirà.

Un'altra da specificare, se si vuole chiudere il programma, avviserà se si vuole chiudere

Se si risponde Sì : la cartella verrà ribloccata e poi il programma si chiuderà

Questo verrà solamente chiesto quando la cartella è stata sbloccata, se la cartella non è stata sbloccata il programma si chiuderà normalmente


- Come Nascondere l'interfaccia grafica

Se la cartella deve rimanere sbloccata, anche il programma deve restare aperto, se l'interfaccia grafica dà fastidio è possibile nasconderla cliccando su Nascondi e verrà mostrata in basso all'area delle notifiche una nuova icona, per riaprire l'interfaccia grafica basta rifare doppio click su di essa

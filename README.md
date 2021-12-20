# StudentWorkingHoursTracker
Aplikacija služi za evidenciju radnih sati studenata. Pogodna je za tvrtke/odjele koji imaju manji broj zaposlenih studenata.
Radi na način:
1. Novi student se dodaje kroz preglednik: Dodavanje studenta

![image](https://user-images.githubusercontent.com/76000546/146838641-e416d3a9-0e6f-44f5-88d8-204a2b661acb.png)
2. Radni status se može editirati kroz preglednik: Pregled studenata, gdje je može odabrati da više ne rade, tj. nisu aktivni(true, false)

![image](https://user-images.githubusercontent.com/76000546/146838915-6814132c-4206-4359-ba6a-c01a7ca8f807.png)
3. Na dnevnoj bazi se unosi radni vrijeme pojedinog studenta. Moguće iz padajućeg izbornika izabrati samo studente koji imaju status Aktivan=true.


![image](https://user-images.githubusercontent.com/76000546/146839295-e516485c-2531-4a09-8dd3-b3b51d825f82.png)
4. U dijelu Statistika se može pregledati ukupan broj radnih sati koje je pojedini student odradio za zadani period.


![image](https://user-images.githubusercontent.com/76000546/146840680-26721437-219c-468e-8c6d-e513be610e84.png)

Priložena je datoteka sql_script.sql koja prikazuje shemu tablica i storanih procedura.

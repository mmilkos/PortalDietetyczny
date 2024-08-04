import { Component } from '@angular/core';

@Component({
  selector: 'app-calculators-grid',
  templateUrl: './calculators-grid.component.html',
  styleUrl: './calculators-grid.component.css'
})
export class CalculatorsGridComponent {

  public isSmallScreen: boolean = false;
  public bmiDesc: string[] =  ["Wskaźnik masy ciała BMI (z ang. body mass index) jest najczęściej stosowaną\n" +
  "metodą do oceny stanu odżywienia u osób dorosłych. Jest to iloraz masy ciała\n" +
  "wyrażonej w kilogramach i wzrostu podniesionego do kwadratu. Jest\n" +
  "najbardziej przydatnym wskaźnikiem nadwagi i otyłości na poziomie populacji,\n" +
  "ponieważ jest taki sam dla obu płci i dla wszystkich grup wiekowych osób dorosłych.\n" +
  "Zaletą tego wskaźnika jest szybkość uzyskania wyniku, jednak nie jest to precyzyjny\n" +
  "miernik, ponieważ może nie odpowiadać temu samemu stopniowi otłuszczenia u\n" +
  "różnych osób.", "Wskaźnik nie uwzględnia takich zmiennych jak wiek, płeć czy budowa ciała. Warto\n" +
  "zwrócić uwagę, że zbyt duża masa ciała, a tym samym wartość wskaźnika BMI\n" +
  "sugerująca nadwagę nie zawsze świadczy o zbyt dużej ilości tkanki tłuszczowej. Taka\n" +
  "sytuacja może wystąpić np. przy znacznie rozbudowanej tkance mięśniowej.", "BMI poniżej 16 oznaza wygłodzenie, " +
  "od 16 do 16.99 wychudzenie, od 17 do 18.49 niedowagę. BMI pomiędzy 18.6 a 24.99 świadczy o wadze prawidłowej. 25 do 29.99 - nadwaga." +
  " BMI 30 - 34.99, 35-39.99 i po wyżej 40 oznacza kolejno otyłość I, II i III stopnia"]

  public whrDesc: string[] =["Wskaźnik WHR (ang. Waist-Hip Ratio) pozwala określić typ sylwetki oraz typ\n" +
  "nadwagi (brzusznej bądź pośladkowo-udowej), dzięki czemu można lepiej dopasować\n" +
  "model żywieniowy do indywidualnych potrzeb. Aby obliczyć wskaźnik WHR należy\n" +
  "dokładnie zmierzyć a następnie podzielić obwód talii (zmierzony w połowie odległości\n" +
  "między dolną częścią ostatniego wyczuwalnego żebra a górną częścią kolca\n" +
  "biodrowego) przez obwód bioder (zmierzony w miejscu największej wypukłości\n" +
  "pośladków).",

    "Współczynnik 0.8 lub wyższy u kobiet i 1.0 lub wyższy u mężczyzn oznacza typ\n" +
    "sylwetki androidalnej, czyli tzw. sylwetki typu jabłko. Współczynnik 0.8 lub niższy u\n" +
    "kobiet i 1.0 lub niższy u mężczyzn oznacza typ sylwetki gynoidalnej, czyli tzw.\n" +
    "sylwetki typu gruszka. Najbliżej idealnym proporcjom sylwetki jest wskaźnik na\n" +
    "poziomie 0.7 u kobiet oraz 1.0 u mężczyzn."  ];



  public ppmDesc: string[] =  ["Kalkulator podstawowej przemiany materii według wzoru Harrisa-Benedicta",

"PPM lub z ang. BMR (ang. basic metabolic rate) to ilość energii (kalorii), jaką potrzebujemy na\n" +
"utrzymanie podstawowych funkcji życiowych organizmu w spoczynku. Jest to minimalna ilość energii\n" +
"jaką człowiek musi dostarczyć organizmowi, aby utrzymać odpowiednią temperaturę ciała,\n" +
"prawidłowe krążenie, zapewnić właściwą pracę układu oddechowego czy układu pokarmowego.\n" +
"Każda osoba ma swój indywidualny wskaźnik PPM, który zależy od wielu czynników, takich jak\n" +
"wiek, płeć, wzrost i masa ciała. Obliczenie swojego zapotrzebowania na kalorie jest kluczowe dla\n" +
"osiągnięcia i utrzymania prawidłowej masy ciała oraz dobrego stanu zdrowia. Wyliczenie PPM to\n" +
"pierwszy krok do wyliczenia całkowitej przemiany materii, czyli CPM."]

  public cpmDesc: string[] =  ["Kalkulator całkowitej przemiany materii pomoże dokładnie obliczyć zapotrzebowanie kaloryczne.",

"CPM (Całkowita Przemiana Materii) to wskaźnik, który informuje, ile energii (kalorii)\n" +
"powinniśmy dostarczać dziennie naszemu organizmowi. Znajomość CPM znacznie ułatwia\n" +
"dobór kaloryczności diety. CPM obliczana jest na podstawie: wieku, płci, masy ciała, wzrostu\n" +
"oraz aktywności fizycznej (rozumianej jako aktywność treningowa oraz tzw. aktywność\n" +
"spontaniczna). Wzór jest iloczynem podstawowej przemiany materii PPM i współczynnika\n" +
"aktywności fizycznej PAL."]

  public nmcDesc: string[] = ["Należną masę ciała można obliczyć, używając wzoru Lorentza.\n" ,
  "\n" +
  "Należna masa ciała (ang. ideal body weight) to inaczej idealna lub pożądana masa ciała.\n" +
  "\n" +
  "Jest to wartość optymalna do zachowania zdrowia i prawidłowego funkcjonowania.\n" +
  "\n" +
  "Uwaga! Wskaźnik ten podaje wartości orientacyjne i nie uwzględnia składu ciała. "]

  ngOnInit(): void
  {
    window.addEventListener('resize', () => {
      this.isSmallScreen = window.innerWidth <= 470;
    });
  }

}

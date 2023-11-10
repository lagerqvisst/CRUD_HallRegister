using System;
using System.Text.Unicode;
using System.IO;

namespace PadelCourts
{
    class PadelCourts
    {
        public static PadelHall[] hallRegister = new PadelHall[0]; // Vektor för att lagra objekt ur klassen PadelHall. Metoden "ExtendArray" ökar antalet fack vid nytt objekt.

        static void Main(string[] args)
        {
            LäsInFrånFil();
            Meny();
        }
        /// <summary>
        /// Visuell metod för att illustrera samtliga menyval.
        /// Fungerar som en del-metod för metoden Meny() som anropas i Main metoden.
        /// Skriver ut en meny som representerar 6 olika val användaren kan välja mellan.
        /// Se metod "Meny()" för fulla funktionalitet.
        /// </summary>
        /// 
        static void ListaMenyVal()
        {
            Console.WriteLine("+-----------------------------+");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("|     PADEL HALLREGISTRET     |");
            Console.ResetColor();
            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("| 1: Lägg till Padelhall".PadRight(30) + "|");
            Console.WriteLine("| 2: Lista Padelhallar".PadRight(30) + "|");
            Console.WriteLine("| 3: Sök efter Padelhall".PadRight(30) + "|");
            Console.WriteLine("| 4: Sortera Padelhallar".PadRight(30) + "|");
            Console.WriteLine("| 5: Ta bort Padelhall".PadRight(30) + "|");
            Console.WriteLine("| 6: Avsluta program".PadRight(30) + "|");
            Console.WriteLine("+-----------------------------+");
        }
        /// <summary>
        /// Hanterar anropningen av samtliga huvudmetoder som listas via menyvalen från metoden "ListaMenyVal"
        /// While-loopen uppfyller funktionen att efter ett menyval aktiverats och körts klart, återvänder användaren till menyn igen.
        /// While loopen kan brytas genom att trycka 6 vilket mostsvarar "Avsluta program."
        /// Felhantering appliceras för att moverka krasch vid inmatning av annat än heltal.
        /// -> Om användaren matar in ett menyval som inte existerar (mellan 1-6) styrs/informeras användaren till korrekt inmatning. 
        /// </summary>
        static void Meny()
        {
            while (true)
            {
                int menyVal;

                ListaMenyVal();

                try
                {
                    menyVal = Convert.ToInt32(Console.ReadLine());

                    if (menyVal == 1)
                    {
                        LäggTillNyHall();
                    }
                    else if (menyVal == 2)
                    {
                        ListaHallar();
                    }
                    else if (menyVal == 3)
                    {
                        SökPåHall();
                    }
                    else if (menyVal == 4)
                    {
                        SorteraHallar();
                    }
                    else if (menyVal == 5)
                    {
                        TaBortHall();
                    }
                    else if (menyVal == 6)
                    {
                        AvslutaProgram();
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error message: Du angav {menyVal} som menyval. \nVänligen välj mellan (1-6).\n");
                        Console.ResetColor();
                    }

                }
                catch (FormatException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error message:  + {e.Message} \nVänligen ange ett heltal mellan 1-5\n");
                    Console.ResetColor();

                    menyVal = 0; // För att fortlöpa loopen
                }
            }
        }
        /// <summary>
        /// Kort visuell metod för att skriva ut i grön text att programmet återgår till huvudmenyn.
        /// Främjar användarvänlighet för att indikera att en metod är klar och användaren kommer tillbaka till huvudmenyn.
        /// Denna anropas när en metod körs klart.
        /// </summary>
        public static void ReturnToMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Återgår till menyn...\n");
            Console.ResetColor();
        }
        /// <summary>
        /// Möjliggör för användaren att lägga till nya objekt av typen Padelhall.
        /// Promptar användaren att mata in värden för respektive attribut i klassen "PadelHall".
        /// Användarinput sparas i respektive klass attribut.
        /// För att få kontinuitet i registrering och främja enklare sök resultat registreras hallnamnet och hallstaden automatiskt till stora bokstäver.
        /// Metoden ExtendArray (beskrivs enskilt längre ner) anropas för att utöka vektorn och placera nya klassobjektet på den platsen.
        /// </summary>
        static void LäggTillNyHall()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Vänligen ange följande för att registrera en ny hall.\n");
            Console.ResetColor();

            PadelHall nyHall = new PadelHall(); //Nytt objekt ur klassen PadelHall

            Console.Write("Vad heter hallen: ");
            nyHall.hallNamn = Console.ReadLine().ToUpper();

            Console.Write("I vilken stad ligger hallen: ");
            nyHall.hallStad = Console.ReadLine().ToUpper();


            int hallBanor;

            do
            {
                Console.Write("Hur många banor har hallen: ");

                if (!int.TryParse(Console.ReadLine(), out hallBanor))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Felaktigt format, ange ett heltal\n");
                    Console.ResetColor();
                }
                else if (hallBanor < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Felaktigt format, ange ett tal större än 0\n");
                    Console.ResetColor();
                }
            }
            while (hallBanor <= 0);

            nyHall.hallBanor = hallBanor;

            int hallBetyg;

            do
            {
                Console.Write("Vilket betyg (1-10) ger du hallen: "); //Felhantering nedan för att förhindra användaren att ge ett betyg under 1 eller över 10
                if (!int.TryParse(Console.ReadLine(), out hallBetyg))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Felaktigt format, ange ett heltal\n");
                    Console.ResetColor();
                }
                else if (hallBetyg < 1 || hallBetyg > 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Felaktigt format, ange ett betyg som är mellan 1-10\n");
                    Console.ResetColor();
                }

            }
            while (hallBetyg < 1 || hallBetyg > 10);


            nyHall.hallBetyg = hallBetyg;


            hallRegister = ExtendArray(hallRegister, nyHall); // Vektorn "hallRegister" ökar sitt fack med "1" och nya objektet ovan lagras på den platsen.

            SkrivTillFil();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{nyHall.hallNamn} har blivit tillagd i hallregistret\n");
            Console.ResetColor();
            ReturnToMenu();
        }
        /// <summary>
        /// Metoden möjliggör att få en utskrift av alla sparade klassobjekt.
        /// Skriver ut varje sparat objekt med respektive attribut på varsin rad.
        /// For-loopen körs X-antal gånger baserat på hur många "fack" vektorn erhåller.
        /// Justering för värdetyper i utskrift tillämpas för attributerna "hallBanor" & "hallBetyg".
        /// </summary>
        static void ListaHallar()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t", "HALL".PadRight(20), "STAD".PadRight(20), "BANOR".PadRight(20), "BETYG".PadRight(20));
            Console.ResetColor();

            //Variabler för att summera utskrift.
            int sumBanor = 0;
            int sumBetyg = 0;
            double avrgBetyg = 0;


            for (int i = 0; i < hallRegister.Length; i++)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t", hallRegister[i].hallNamn.PadRight(20),
                                                          hallRegister[i].hallStad.PadRight(20),
                                                          hallRegister[i].hallBanor.ToString().PadRight(20),
                                                          hallRegister[i].hallBetyg.ToString().PadRight(20));

                sumBanor += hallRegister[i].hallBanor;
                sumBetyg += hallRegister[i].hallBetyg;

            }
            avrgBetyg = (sumBetyg / hallRegister.Length);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"TOTALA BANOR: {sumBanor}\nGENOMSNITTLIGT BETYG: {avrgBetyg}\n");
            Console.ResetColor();
            ReturnToMenu();
        }
        /// <summary>
        /// Metoden ExtendArray ger oss möjligheten att inte behöva "hårdkoda" vektorn hallRegister.
        /// Med hjälp av en workaround skapas en temporär vektor som erhåller ett till "fack" än hallRegister.
        /// For-loopen kopierar över objekten från befintliga vektorn.
        /// Eftersom vi har en extra ledig plats kvar i temporära vektorn kan vi mata in ett nytt objekt som kommer från "LäggTill() metoden.
        /// Se LäggTill() metoden rad 119 där nedanstående metod anropas.
        /// Slutligen kopierar den ordninarie vektorn (hallRegister) över information från temporära.
        /// </summary>
        static PadelHall[] ExtendArray(PadelHall[] hallRegister, PadelHall newItem)
        {
            PadelHall[] tempVektor = new PadelHall[hallRegister.Length + 1];
            //Skapar temporär vektor i metod för att utöka platsen på befintlig
            //- vektor med en plats

            for (int i = 0; i < hallRegister.Length; i++)
            {
                tempVektor[i] = hallRegister[i]; //Kopierar över objekt till temp vektor
            }

            tempVektor[hallRegister.Length] = newItem; //Nya objektet från LäggTill Metoden placeras i nya facket

            return tempVektor;
        }
        /// <summary>
        /// Denna metod använder sig av linjär-sökning för att ge möjlighet för användaren att ta bort lagrade objekt. 
        /// För att främja användarvänlighet skrivs alla sparade objekt (endast hallnamnet (hallNamn) ->
        /// Namnen skrivs ut eftersom vi använder oss av "Equals" i identifering och användaren behöver mata in det exakta namnet.
        /// Användaren promptas att ange vilken hall hen vill ta bort.
        /// For-loopen kör sekeventiellt igenom samtliga fack i vektorn och stannar utifall användareinputen hittades på ett "fack" i vektorn.
        /// Stannar loopen, sparar vi ner "fack"-positionen med hjälp av en int som agerar som ett index.
        /// Sista for-loopen kör sekventiellt igenom vektorn men startar vid fack-positionen som representeras av index.
        /// For-loopen flyttar facken ett steg till höger och effektivt skriver över facket som har position "index".
        /// Detta tillvägagångssätt är likt när vi sorterar vektorer, men i detta syfte vill vi skriva över värdet (i sortering använder vi swap där vi temporärt sparar data)
        /// Sista facket i vektorn är vid loopens slut tomt.
        /// Metoden DecreaseArray anropas nu för att ta bort sista facket som står tomt. (Denna metod beskrivs nedan enskilt (Rad 285)).
        /// Det som beskrivs ovan är relevant till raderingen men det återfinns felhantering och annan användarvänlighet som att ångra/återgå till meny.
        /// </summary>
        public static void TaBortHall()
        {
            for (int i = 0; i < hallRegister.Length; i++)
            {
                Console.WriteLine(hallRegister[i].hallNamn);
            }

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Tryck 'N' för att avbryta\nVilken hall vill du ta bort: ");
                Console.ResetColor();

                string hallSearch = Console.ReadLine().ToUpper(); //Alla nya objektet registreras i CAPS, skriver användaren in med små bokstäver spelar det ingen roll.

                if (hallSearch == "N")
                {
                    Console.WriteLine("Avbryter...\n");
                    ReturnToMenu();
                    break;
                }
                else
                {
                    int index = -1;
                    for (int i = 0; i < hallRegister.Length; i++) //Letar efter vilken plats i vektorn hallnamnet finns.
                    {
                        if (hallRegister[i].hallNamn.Equals(hallSearch))
                        {
                            index = i;
                            break; //Bryter ur loopen och ger oss värdet "i" vilket reflekterar plats i vektorn.
                        }

                    }
                    if (index == -1) // index kommer förbli vid sitt orginella värde om for loopen ovan körs klart utan att bryta tidigare.Vilket betyder att userinputen inte återspeglar något hallnamn i vektorn.
                    {
                        Console.WriteLine($"Det finns ingen hall som heter {hallSearch} \n");
                        ReturnToMenu();
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"ÄR DU SÄKER PÅ ATT DU VILL TA BORT: [{hallSearch}] ?\n");
                        Console.Write("BEKRÄFTA BORTTAGNING MED 'J' - ÅNGRA MED VALIFRI TANGENT: ");
                        Console.ResetColor();

                        string confirm = Console.ReadLine().ToUpper();

                        if (confirm == "J")
                        {
                            for (int i = index; i < hallRegister.Length - 1; i++) //
                            {
                                hallRegister[i] = hallRegister[i + 1]; //Objektet som ska tas bort, ersätts med objektet "höger om sig".
                            }

                            hallRegister = DecreaseArray(hallRegister);
                            SkrivTillFil();

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"{hallSearch} har tagits bort från registret\n");
                            ReturnToMenu();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Radering ångrad\n");
                            ReturnToMenu();
                            break;
                        }
                    }
                }

            }
        }
        /// <summary>
        /// Samma logik/princip som beskrivs i metoden LäggTillNyHALL() ovan. (rad 119 för referens)
        /// Metoden anropas efter ett objekt har tagits bort via metoden "TaBortObjekt" vilket frigör facket längst bak i vektorn hallRegister.
        /// En temporär vektor skapas som erhåller ett mindre "fack" än ordinarie (hallRegister).
        /// Kopierar över alla objekt från ordinarie.
        /// Avslutas med att ordninare vektor kopierar temporära vektorn. 
        /// </summary>
        public static PadelHall[] DecreaseArray(PadelHall[] hallRegister)
        {
            PadelHall[] tempVektor = new PadelHall[hallRegister.Length - 1];

            for (int i = 0; i < hallRegister.Length - 1; i++)
            {
                tempVektor[i] = hallRegister[i]; //kopierar över objekt
            }

            return tempVektor;
        }
        /// <summary>
        /// Metoden möjliggör att spara data från en session till en textfil så den kan användas och laddas in via metoden LäsInFrånFil()
        /// Öppnar en befintligt/skapar en ny textfil via StreamWriter.
        /// For-loopen skriver radvis ut respektive sparade objekt till textfilen med sk. "Tab" mellanrum så det ser visuellt bra ut.
        /// Att skriva ut med "tab" mellanrum uppfyller även funktionen att lättare kunna läsa in den sparade datan igen via metoden LäsInFrånFil (rad 333)
        /// Filen stängs när loopen är klar. 
        /// </summary>
        public static void SkrivTillFil()
        {
            StreamWriter utfil = new StreamWriter("hallar.txt");

            for (int i = 0; i < hallRegister.Length; i++)
            {

                utfil.Write("{0}\t{1}\t{2}\t{3}\t",
                    hallRegister[i].hallNamn,
                    hallRegister[i].hallStad,
                    hallRegister[i].hallBanor,
                    hallRegister[i].hallBetyg);

                utfil.WriteLine();
            }
            utfil.Close();

        }
        /// <summary>
        /// Metoden möjliggör att läsa in tidigare sparad data från en textfil.
        /// Öppnar den skapade textfilen från metoden SkrivTillFil() (rad 304)
        /// While-loopen används för att kunna skanna igenom rad för rad i textfilen och avslutas när en rad är tom (null).
        /// Genom metoden Split konverterar vi bort "tab" mellanrummet och skriver endast in attributdatan tillbaka i klass-vektorn (hallRegister).
        /// Genom en vektor av typen string samt med hjälp av Split-metoden kan vi dela upp inläsningen i fyra "block" som representerar de fyra klassattributerna från klassen "PadelHall".
        /// Via samma metodik som en ny hall registreras via LäggTillNyHall() metoden anropar vi ExtendArray-metoden under varje iteration i while-loopen.
        /// </summary>
        public static void LäsInFrånFil()
        {
            StreamReader infil = new StreamReader("hallar.txt");

            string rad;

            while ((rad = infil.ReadLine()) != null)
            {
                PadelHall LoadHall = new PadelHall();

                string[] fält = rad.Split('\t');
                LoadHall.hallNamn = fält[0];
                LoadHall.hallStad = fält[1];
                LoadHall.hallBanor = Convert.ToInt32(fält[2]);
                LoadHall.hallBetyg = Convert.ToInt32(fält[3]);

                hallRegister = ExtendArray(hallRegister, LoadHall);

            }
            infil.Close();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{hallRegister.Length} objekt har lästs in från hallar.txt...\n");
            Console.ResetColor();
        }
        /// <summary>
        /// Nedanstående är en "huvud"-metod för att hantera sortering.
        /// Mer detaljerad beskrivning av hur sorteringen utförs finns dokumenterat över metoden "BubbleSort", "Exchangesort" & "Swap"
        /// Likt huvudmenyn i detta program anropas en metod för att printa ut menyvalen vilket ger användaren möjlighet att sortera på samtliga klassatributer som klassen PadelHall erhåller.
        /// För att demonstrera att vi behärskar både Bubble- & ExchangeSort har vi valt att dela upp så att två attributer (hallBanor & hallBetyg) sorteras med BubbleSort medans de andra sorteras med Exchange.
        /// </summary>
        public static void SorteraHallar()
        {
            int menyVal;

            while (true)
            {
                SorteringMeny();

                try
                {
                    menyVal = Convert.ToInt32(Console.ReadLine());

                    if (menyVal == 1 || menyVal == 2)
                    {
                        BubbleSort(hallRegister, menyVal);
                    }
                    else if (menyVal == 3 || menyVal == 4)
                    {
                        ExchangeSort(hallRegister, menyVal);
                    }
                    else if (menyVal == 5)
                    {
                        break;
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error message: Du angav {menyVal} som menyval. \nVänligen välj mellan (1-5).\n");
                        Console.ResetColor();
                    }
                }
                catch (FormatException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error message:  + {e.Message} \nVänligen ange ett heltal mellan 1-5\n");
                    Console.ResetColor();

                    menyVal = 0;
                }
            }
        }
        /// <summary>
        /// Denna metod tillåter användaren att söka i fritext efter en hall.
        /// Användarinmatningen sparas i string-variabeln "search".
        /// En for-loop körs sekventiellt igenom klass-vektorn.
        /// Under iterationen ses ett villkor över vilket innefattar att se om "search" existerar i attributen hallNamn eller hallStad.
        /// Uppfylls villkoret skrivs all klassdata ut i en rad.
        /// Kod återanvänds från ListaHallar() för att utskrift ska vara konsekvent vid listning av hallar.
        /// För användarvänlighet har vi implementerat en bool (hittatResultat) samt en "räknare" som adderar varje gång loopen hittar ett resultat.
        /// -> Detta gör att användaren förstår exempelvis om hens sökning producerade ett resultat eller ej. 
        /// Vi vill motivera användadet av "Contains" i denna sökmetod för att främja användarvänlighet då klassattributen hallNamn kan inneha komplexa namn.
        /// Vi ser att det hämmar användarvänligheten om vi applicerar "Equals" i detta fall. 
        /// </summary>
        public static void SökPåHall()
        {

            Console.Write("Sök efter en hall: ");
            string search = Console.ReadLine().ToUpper();
            bool hittatResultat = false;
            int antalResultat = 0;

            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t", "HALL".PadRight(20), "STAD".PadRight(20), "BANOR".PadRight(20), "BETYG".PadRight(20));

            for (int i = 0; i < hallRegister.Length; i++)
            {
                if (hallRegister[i].hallNamn.Contains(search) || hallRegister[i].hallStad.Contains(search))
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t", hallRegister[i].hallNamn.PadRight(20),
                                                          hallRegister[i].hallStad.PadRight(20),
                                                          hallRegister[i].hallBanor.ToString().PadRight(20),
                                                          hallRegister[i].hallBetyg.ToString().PadRight(20));
                    antalResultat++;
                    hittatResultat = true;
                }

            }
            if (!hittatResultat)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Din sökning gav inget resultat.\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Din sökning gav {antalResultat} resultat.\n");
                Console.ResetColor();
            }

            ReturnToMenu();
        }
        /// <summary>
        /// Metoden används i sorteringsmetoderna som beskrivs nedan (BubbleSort & ExchangeSort).
        /// Syftet är att kunna byta plats på två objekt i vektorn.
        /// För att förhindra att vi effektivt skriver över ett fack i vektorn, tillämpar vi användningen av ett temporärt objekt.
        /// På rad 518 skrivs informationen över på vektor[a]. Genom att vi först temporärt lagrar den i tmp förhindrar vi överskrivandet.
        /// Denna metod ekekveras exempelvis på rad 543 (BubbleSort) och 587 (ExchangeSort)
        /// </summary>

        static void Swap(PadelHall[] vektor, int a, int b)
        {
            PadelHall tmp = vektor[a];
            vektor[a] = vektor[b];
            vektor[b] = tmp;
        }
        /// <summary>
        /// BubbleSort som sorteringsalternativ appliceras på attributerna hallBetyg & hallBanor.
        /// Metoden gör det möjligt via "huvud"-metoden SorteraHallar att via användarinmatning (menyVal) aktivera den sortering som korrosponderar med meny/sorteringsalternativen.
        /// BubbleSort består i grunden av att jämföra två värden och byta plats på varandra om ett villkor är sant.
        /// Den yttre for-loopen dikterar hur många gånger den inre for-loopen ska köras.
        /// Den inre for-loopen utför själva sorteringen. Ex. Fack [0] i vektorn jämförs med Fack [1]. Är Fack [0] mindre än fack [1] byter dom plats i vektorn.
        /// I det här fallet gör det att objekten som har högst värde på hallBanor (ints) sorteras först i vektorn.
        /// Själva "plats-bytet" genomförs via metoden Swap som beskrivs enskilt på rad 508.
        /// </summary>
        static void BubbleSort(PadelHall[] vektor, int sorteringsVal)
        {

            for (int i = 0; i < hallRegister.Length - 1; i++) // (-1) för vi kommer jämföra med ett [+1] värde vilket kommer kolla på sista facket. Annars får vi index out of bounds error.
            {
                for (int j = 0; j < (hallRegister.Length - i) - 1; j++)
                {
                    if (sorteringsVal == 1 && hallRegister[j].hallBanor < hallRegister[j + 1].hallBanor)
                    {
                        Swap(hallRegister, j, j + 1);
                    }
                    else if (sorteringsVal == 2 && hallRegister[j].hallBetyg < hallRegister[j + 1].hallBetyg)
                    {
                        Swap(hallRegister, j, j + 1);

                    }

                }
                ListaHallar();

            }
        }
        /// <summary>
        /// Likt det som är beskrivet under BubbleSort på rad 522 uppfyller denna metod samma funktion. Vi tillämpar denna metod för att visa att vi behärskar olika typer av sorteringar.
        /// Medans BubbleSort alltid jämför det närmst liggande värder till höger om sig, jämför ExchangeSort samtliga värden höger om sig under en iteration.
        /// Jämfört med BubbleSort så utförs sorteringen i Exchange först när vi har jämfört alla värden i vektorn. Kontra BubbleSort som sorterar efter varje jämförelse i den inre loopen.
        /// </summary>
        static void ExchangeSort(PadelHall[] vektor, int sorteringsVal)
        {

            for (int i = 0; i < hallRegister.Length; i++)
            {
                int minst = i;

                for (int j = i + 1; j < hallRegister.Length; j++)
                {
                    if (sorteringsVal == 3 && string.Compare(hallRegister[minst].hallNamn, hallRegister[j].hallNamn) > 0)
                    {
                        minst = j;
                    }
                    else if (sorteringsVal == 4 && string.Compare(hallRegister[minst].hallStad, hallRegister[j].hallStad) > 0)
                    {
                        minst = j;
                    }
                }

                if (i < minst)
                {
                    Swap(hallRegister, minst, i);
                }
            }
            ListaHallar();

        }
        /// <summary>
        /// Visuell metod som anropas i metoden SorteraHallar.
        /// Koden nedan skriver ut samtliga sorteringsalternativ som användaren kan välja på. 
        /// Samma format används likt ListaMenyVal (rad 23)
        /// </summary>
        public static void SorteringMeny()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("|     VÄLJ SORTERING    |");
            Console.ResetColor();
            Console.WriteLine("| 1: Sortera efter antal banor".PadRight(30) + "|");
            Console.WriteLine("| 2: Sortera efter betyg".PadRight(30) + "|");
            Console.WriteLine("| 3: Sortera efter hallnamn".PadRight(30) + "|");
            Console.WriteLine("| 4: Sortera efter hallstad".PadRight(30) + "|");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("| 5: Återgå till huvudymeny".PadRight(30) + "|");
            Console.ResetColor();
            Console.WriteLine("+-----------------------------+");

        }
        /// <summary>
        /// Visuell metod som anropas i Meny-metoden.
        /// Denna anropas när användaren manuellt väljer att avsluta programmet.
        /// Denna metod ger en visuell konfirmation på hur många objekt som sparats till text-filen innan programmet avslutas
        /// </summary>
        public static void AvslutaProgram()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{hallRegister.Length} objekt har sparats i hallar.txt... \nAvslutar program...");
            Console.ResetColor();

        }

    }
}




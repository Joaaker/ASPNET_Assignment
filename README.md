Inlämningsuppgift - ASP.NET


I denna inlämningsuppgift ska du bygga en webbapplikation med hjälp av ASP.NET Core. Det är ditt ansvar att se till att din webbapplikation uppfyller kraven för godkänt respektive väl godkänt. Ja det är stora skillnader kunskapsmässigt mellan godkänt och väl godkänt. Detta enligt nya riktlinjer från EC och Myndigheten för Yrkeshögskolan, samt att väl godkänt är det högsta betyget du kan få och du ska självständigt och på ett fördjupat sätt kunna bevisa dina kunskaper enligt kursplanen.



Användning av AI-genererad kod
Om du använder dig av AI-genererade kod-stycken, från exempelvis ChatGPT eller Claude AI, så måste detta framgå tydligt. Kodstycket ska då innehålla en kommentar som talar om att det är genererad kod, annars anses det vara plagiat och du kommer få underkänt som betyg. Men tänk på att skriva din egna kod så du lär dig skriva kod. Det är A och O när det kommer till programmering.



Val av lagringslösning
Du väljer själv vilken typ av databasmotor du vill använda, exempelvis: Sqlite, LocalDB eller SQL Server. Välj en lösning som passar bäst för din plattform och ditt ändamål. Du ska lägga in din connectionstring till din databas i appsettings.json. När du sedan laddar upp din webbapplikation på GitHub så kommer inte appsettings.json eller din databas att skickas med och det är så det ska vara.


Testning av din kod
Nej, du behöver inte göra några tester på din kod, varken enhetstester eller integrationstester. Du får absolut om du vill. Jag förespråkar att du ska göra tester då du kommer göra tester på all din kod ute i arbetslivet, MEN för denna uppgift så behöver du inte göra några tester alls.



BETYGSKRITERIER FÖR INLÄMNINGSUPPGIFTEN
Här nedan specificeras vad som krävs för att uppnå godkänt respektive väl godkänt. Det är viktigt att notera att det är ett stort steg mellan dessa nivåer, vilket följer de riktlinjer som EC-Utbildning och Myndigheten för Yrkeshögskolan nu tillämpar.



FÖR GODKÄNT KRÄVS FÖLJANDE:

Webbapplikationen kan vara av typen ASP.NET - Razor Pages eller ASP.NET - MVC och ska innehålla följande krav som specificeras upp här och designfilen ska följas. Om det inte står här så ska det inte vara med:



Du ska samtliga sidor för godkänt som finns med i designfilen.
Det ska gå att visa samtliga projekt och det ska gå att visa dessa beroende på om det är startade eller slutförda. Vilket gör att du kommer behöva hantera status själv trots att det inte är med i designen.
Du kan behöva lägga till andra fält för ett projekt om det behövs.
Det ska gå att skapa ett nytt projekt och det ska gå att uppdatera ett projekt.
Du kan lägga till viss data manuellt i databasen om det skulle behövas men projekt och användare ska läggas in via formulären.
All formulärdata måste valideras med hjälp av Javascript, välj lämplig valideringsmetod för att säkerställa att datat som skickas är korrekt.
All data ska hanteras i en valfri lagringslösning och du ska använda Entity Framework Core och ska tillämpa lämpliga designmönster som exempelvis Service Pattern.
Du ska använda dig av Microsoft Identity (Individual Account) för grundläggande åtkomst- och behörighetskontroll, vilket innebär att det ska kunna gå att registrera sig och att logga in, samt användarspecifika sidor måste skyddas med hjälp av authorize-attributet. Ingen rollhantering behövs.
Du kommer själv behöva lista ut med dina tidigare kunskaper och färdigheter från tidigare kurser hur man löser vissa delar. Viss handledning kommer ges via lektioner och tips och trix.




FÖR VÄL GODKÄNT KRÄVS FÖLJANDE:

Webbapplikationen måste vara av typen ASP.NET - MVC och ska innehålla följande krav som specificeras upp här. Du kommer här behöva fatta självständiga beslut över hur du ska genomföra vissa delar samt vad som ska finnas med även om inte allt specificeras upp i detalj här. Det är en del av VG-nivån enligt kursplanen:



Du ska samtliga sidor för väl godkänt som finns med i designfilen.
Du ska använda dig av Views och Partial Views samt ViewModels där det är lämpligt.
Du ska utifrån designfilen självständigt kunna se vad som behöver byggas upp och finnas med för att få sidan att fungera enligt designfilen.
Om data måste läggas in såsom olika statuslägen så måste det finnas en extra sida, som bara en administratör kan komma åt, där denna information läggs in och administreras.
All formulärdata måste valideras med hjälp av både Javascript och ModelState.
Webbapplikationen måste använda sig av Cookie Consent.
Det ska finnas ett separat data-lager och separat business-lager.
All data ska hanteras i en valfri lagringslösning och du ska använda Entity Framework Core och ska självständigt tillämpa lämpliga designmönster.
Du ska använda dig av Microsoft Identity (Individual Account) för åtkomst- och behörighetskontroll, samt sidor måste skyddas på lämpligt sätt.
Användare ska ha en standard roll och endast användare som är administratörer ska kunna lägga till och ta bort data bortsett från projekt.
Alternativt inloggningssätt måste finnas, som exempelvis Google, Meta och X.
Det måste gå att växla mellan mörkt och ljus tema.
Det ska vara möjligt att få notifieringar när nya projekt har lagts till. Samt användare för administratörer.
Det måste gå att lägga till en bild för ett projekt och det måste gå att lägga till tillgängliga medlemmar på ett projekt.

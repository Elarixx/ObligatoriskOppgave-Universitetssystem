# Obligatorisk Oppgave 1-Universitetssystem


## Oppgave 1:
Du skal utvikle en C# Console applikasjon som modellerer et universitetssystem. Fokuset er objektorientert design, god struktur og riktig bruk av C# konstruksjoner.

Applikasjonen skal håndtere:
- Brukere: studenter (inkludert utvekslingsstudenter fra andre universiteter) og ansatte
- Kurs: oppretting, påmelding, og oversikt
- Bibliotek: utlån/innlevering, tilgjengelighet, og historikk

 

### Funskjonaliteter som må implementeres:
#### 1) Brukere, applikasjonen skal støtte minst disse rollene:

**Student**
- Student-ID
- Navn
- E-post
- Liste over kurs studenten er meldt opp i

**Utvekslingsstudent**
- Hjemuniversitet
- Land
- Periode (fra–til)

**Ansatt**
- Ansatt-ID
- Navn
- E-post
- Stilling (f.eks. foreleser, bibliotekar, administrasjon)
- Avdeling


#### 2) Kurs, applikasjonen skal kunne:
- Opprette kurs (kode, navn, studiepoeng, maks antall plasser)
- Melde student på kurs (sjekk kapasitet)
- Melde student av kurs
- Liste kurs og deltakere
- Søke etter kurs basert på kriterier (kode og navn)

 
#### 3) Bibliotek, applikasjonen skal kunne:
- Registrere bøker/medier (id, tittel, forfatter, år, antall eksemplarer)
- Låne ut og levere inn (Lån må knyttes til en bruker enten student eller ansatt).
- Hindre utlån hvis ingen eksemplarer er tilgjengelig
- Vise aktive lån og historikk

 
### Applikasjonen skal ha følgende menyen:

[1] Opprett kurs

[2] Meld student til kurs

[3] Print kurs og deltagere

[4] Søk på kurs

[5] Søk på bok

[6] Lån bok

[7] Returner bok

[8] Registrer bok

[0] Avslutt 

---
## Oppgave 2:
**Utvid Oppgave 1 med følgende elementer:**

- Du skal forbedre koden slik at applikasjonen først spør om brukeren eksisterer eller ny, hvis den eksisterer, da ber applikasjonen om å logge inn med brukernavn og passord. Hvis den er ny, da må den registrere seg. 
- Du skal forbedre koden slik at når brukeren logger seg inn får den funksjonalitetene den skal bruk basert på sin rolle (student eller bibliotek ansatte eller fag lærer osv).
- Du skal forbedre koden slik at faglæreren skal kunne opprette et kurs, søke på kurs, søke på bøker, låne bøker, sette karakter til studentene i kursene den underviser og registrere pensum til kursene.
- Du skal forbedre koden slik at faglæreren ikke kan registrere et kurs med samme kurskode/navn flere ganger og at en student ikke kan melde seg på samme kurset flere ganger.
- Du skal forbedre koden slik at studenter og faglærere skal kunne levere lånte bøker tilbake.
- Du skal forbedre koden slik at bibliotek ansatte skal kunne registrere bøker, se på aktive lån og historikk.
- Du skal forbedre koden slik at studenten skal kunne melde seg på/av kurs, se på karakter den fikk, søke på bøker, låne bøker og se på kurs den er meldt på.
- Du skal implementere feilhåndtering for aktuelle feil som kan oppstå.
- Du skal lage 3 til 4 enhetstester for applikasjonen (minst 3 og maks 4 tester).
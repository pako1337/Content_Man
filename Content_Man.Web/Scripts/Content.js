﻿angular.module('Content', [])
    .config(function ($routeProvider) {
        $routeProvider
            .when('/', { controller: ContentList, templateUrl: 'ContentList.html' })
            .when('/edit/:contentId', { controller: ContentEdit, templateUrl: 'ContentEdit.html' })
            .otherwise({ redirectTo: '/' });
    });

function ContentList($scope) {
    $scope.contentElements = [
        { contentId: 1, Language: 'English', Category: 'Generic', Value: "Na pokładowym chronometrze zwanej przez załogę Latającą Holerą transgalaktycznej pirackiej łajby Małpilus dochodziła godzina dwudziesta pierwsza, gdy wydarzył się ten wypadek. Sam w sobie nie miał wielkiego znaczenia - ot, drobny błąd nawigatora, cała sprawa mogłaby spokojnie obyć się bez konsekwencji. Traf chciał jednak, iż autorzy uczonych ksiąg, które miały dopiero powstać, uznali tę właśnie chwilę za przełomową w dziejach Trzeciej Ery. Nam zaś wszakże nie wypada się spierać z historią. Zamilknijmy przeto i pozwólmy jej mówić własnymi słowy... " },
        { contentId: 2, Language: 'English', Category: 'Generic', Value: "Planetoida pojawiła się znienacka i nie wiadomo skąd. Wychynęła z mroku z prędkością teraźniejszości, jak zapewne określiłby to poetycko któryś z pierwszych bywalców forum „Science Fiction, Fantasy i Horror” (z czasów zanim jeszcze periodyk wszedł do kanonu akademickich podręczników). Jej chropowata kulista powierzchnia majaczyła teraz gdzieś w okolicach przedniej szyby, cały czas niepokojąco przybierając na wielkości. Tutaj trzeba było działać szybko. " },
        { contentId: 3, Language: 'English', Category: 'Generic', Value: "Z mesy wybiegło kilka malowniczych postaci, które przy odrobinie tolerancji można było wziąć za oficerów. Potrącając się w pośpiechu dobiegły korytarzem do sterowni. Nastąpiła mała przepychanka, rozległo się kilka zduszonych przekleństw. Ktoś wykonał piruet na rozmazanym po podłodze maśle orzechowym. W końcu jednemu z mężczyzn udało się sprytnie wyślizgnąć z grupki walczących i zawładnąć fotelem pilota. Przełączył sterowanie na ręczne i gwałtownie skręcił w bok, ratując okręt przed zawarciem bliskiej znajomości z planetoidą. Tymczasem pozostali piraci głucho odbijali się od ścian, jedno po drugim lądując na podłodze. Pancerne drzwi otworzyły się po raz drugi, tym razem wylatując z zawiasów. Do sterowni wbiegło siedmiu rozespanych majtków w różowych majtkach, którzy poczęli w panice biegać dookoła. Holera stęknęła, zatrzeszczała i wyhamowała. Gdy zaś przebrzmiała już kanonada wypadających z rozmaitych skrytek torebek, paczuszek i puszek z podejrzaną chlupiącą zawartością, nastała wreszcie względna cisza. Upragniona, aczkolwiek nie przez wszystkich. W kosmicznej próżni zawisło nieuchronne pytanie. " },
        { contentId: 4, Language: 'English', Category: 'Generic', Value: "-	Do stu tysięcy SuperNowych: nawigatorze, co to było? " },
        { contentId: 5, Language: 'English', Category: 'Generic', Value: "-	Ee, melduję, że planetoida, panie kapitanie – odparł niepewnie zapytany mężczyzna masując stłuczony podczas upadku łokieć. " },
        { contentId: 6, Language: 'English', Category: 'Generic', Value: "-	Tyle to i ja wiem, QCF! Darujmy sobie oficjalne tytuły; zamiast tego wyjaśnij nam gdzie właściwie jesteśmy i jakim cudem ten kosmiczny śmieć znalazł się na naszej drodze. " }
    ];
}

function ContentEdit($scope, $routeParams) {
    $scope.contentId = $routeParams.contentId;
}
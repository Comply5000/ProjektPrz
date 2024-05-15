<template>
    <NavBar />
    <!-- <Space /> -->
  <div class="offer-container">
    <div class="offer-main">
      <div class="offer-company">
        <div class="offerleft">
          <h3 class="offer-name">{{ offer.offerName }}</h3>
          <p class="company-name">{{ offer.companyName }}</p>
          <p>Typ: <span class="offer-type">{{ offer.companyType }}</span></p>
          <p>Data wystawienia: {{ offer.issueDate }}</p>
          <p>Data ważności: {{ offer.expiryDate }}</p>
          <p>Ocena: 4/5</p>
          <p>Lokalizacja: Rzeszów</p>
        </div>
        <div class="offer-image">
          <img :src="offer.image" :alt="'Offer Image ' + (index + 1)">
        </div>
      </div>
      <div class="offeropis">
        Odkryj świat flamingów w naszym sklepie zoologicznym!

Marzysz o posiadaniu tych pięknych, egzotycznych ptaków? Teraz Twoje marzenie może stać się rzeczywistością! W naszym sklepie zoologicznym oferujemy szeroki wybór flamingów, które zachwycą Cię swoim kolorowym pióropuszem i elegancją.

W naszej ofercie znajdziesz różnorodne gatunki flamingów, dopasowane do Twoich preferencji i możliwości. Niezależnie od tego, czy szukasz flamingów karłowatych czy większych gatunków, u nas znajdziesz to, czego potrzebujesz.

Ale to nie wszystko! Wraz z naszymi pięknymi flamingami oferujemy również pełen zakres akcesoriów i produktów do pielęgnacji, które pomogą Ci stworzyć idealne warunki życia dla Twoich ptaków. Znajdziesz u nas karmniki, legowiska, specjalne karmy i wiele innych niezbędnych akcesoriów.

Ale to jeszcze nie koniec! Nasz doświadczony zespół ekspercki służy pomocą w wyborze, opiece i pielęgnacji Twoich flamingów. Zapewniamy fachowe porady i wsparcie, aby Twoje flamingi były zawsze zdrowe i szczęśliwe.

Odkryj pasję flamingów w naszym sklepie zoologicznym już dziś! Zostań częścią naszej społeczności miłośników egzotycznych ptaków i ciesz się pięknem natury w swoim domu.
      </div> <!-- To jest puste, tutaj będzie zawartość opisu -->
    </div>
</div>
  </template>

  <script>
  import NavBar from '@/components/NavBar.vue';
  import Space from '@/components/Space.vue';

  export default {
    components: {
      NavBar,
      Space
    },
    data() {
      return {
        offer: {
        offerName: 'Oferta flamingowa',
        companyName: 'Flaming sp zoo',
        companyType: 'zoologiczny',
        issueDate: '2002-12-27',
        expiryDate : '2024-05-15',
        image: "../src/views/offers/flamingo.jpg"
      }
      };
    },
    methods: {
      changePage(page) {
        if (page >= 1 && page <= this.pageCount) {
          this.currentPage = page;
        }
      }
    }
  };
  </script>

  <style scoped>


.offer-left h3
{
  padding:115px;
}
 
  
  .offer-type {
    padding: 5px 10px;
    border-radius: 5px;
    text-align: center;
  }

.offer-image img {
    width: 275px;
    height: 275px;
    margin-top: 4px;
    border-radius: 10px;
     /* Zielona ramka o szerokości 2px */
}

.offerleft p
{
  margin: -4px;
}
  .offer-name {
    background-color: #28a745;
    color: white;
    padding: 5px 10px;
    border-radius: 5px;
    text-align: center;
  }
  .offer-container {
  background-color: rgb(40, 167, 69);
  height: 100vh;
  display: flex;
  align-items: center;
  flex-direction: column;
  margin-top: 80px;
}

.offer-main {
  background-color: #ddd;
  color: white;
  display: flex;
  flex-direction: column;
  
  align-items: center;
  height: 90%;
  width: 80%;
  padding: 25px;
  padding-top: 55px;
  text-align: center;
}

.offer-company
{
  width:90%;
   max-height:300px;
 
  float: left;
  flex-grow: 1;
  background-color: white;
  font:bold;
  font-size:20px;
  display:flex;
}
.offerleft {
  background-color: white;
  color: black;
  text-align: center;
  width: 70%;
}

.offer-image {
  width: 30%;
  
  
}

.offeropis {
  background-color: white;
  color:black;
  
  height:500px;
  width: 90%;
  font-size:20px;
  font:bold;
}
</style>

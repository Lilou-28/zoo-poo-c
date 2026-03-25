# zoo-poo-c
Zoo Management Simulator (C# - Console)
Description

Ce projet est une simulation de gestion de zoo en ligne de commande. Vous incarnez le directeur d’un zoo et devez gérer :

Les animaux (achat, vente, reproduction, soins)
Les habitats (achat, vente, capacité)
L’alimentation et les budgets
Les visiteurs et les revenus
Les événements aléatoires et exceptionnels

Le jeu se déroule mois par mois, avec des saisons, des probabilités de maladies, de naissance, de mortalité et de catastrophes.

Animaux

Chaque espèce est définie avec plusieurs caractéristiques :

Espèce	Alimentation	Quantité/jour	Jours avant faim	Reproduction	Maturité sexuelle	Gestation	Fin de reproduction	Mortalité infantile	Espérance de vie	Remarques
Tigre M	Viande	12 kg	2	–	6 ans	–	14 ans	–	25 ans	–
Tigre F	Viande	10 kg	2	3 par portée / max 1 portée tous les 20 mois	4 ans	3 mois	14 ans	33%	25 ans	Femelle gestante mange 2x +, pas exposée au public
Aigle M	Viande	0,25 kg	10	–	4 ans	–	14 ans	–	25 ans	Fidèle
Aigle F	Viande	0,3 kg	10	2 œufs en mars	4 ans	45 jours	14 ans	50%	25 ans	Fidèle
Poule	Graines	0,15 kg	1	200 œufs/an	6 mois	6 semaines	8 ans	50%	15 ans	–
Coq	Graines	0,18 kg	2	–	6 mois	–	8 ans	–	15 ans	–

Les femelles ne se reproduisent pas si elles ont faim. Si elles sont en gestation et manquent de nourriture, elles perdent le foetus.

Nourriture
Aliment	Prix / kg
Viande	5 €
Graines	2,5 €
Achat / Vente d’animaux
Animal	Prix achat	Prix vente
Tigre 6 mois	3 000 €	1 500 €
Tigre 4 ans	120 000 €	60 000 €
Tigre 14 ans	60 000 €	10 000 €
Poule 6 mois	20 €	10 €
Coq 6 mois	100 €	20 €
Aigle 6 mois	1 000 €	500 €
Aigle 4 ans	4 000 €	2 000 €
Aigle 14 ans	2 000 €	400 €

Les animaux adultes ne se reproduisent pas le premier mois suivant leur arrivée.

Habitats
Espèce	Prix achat	Prix vente	Capacité	Surpopulation	Probabilité maladie
Tigre	2 000 €	    500 €	    2	         50% / mois	      20%
Aigle	2 000 €	    500 €	    4	         50% / mois	      10%
Poules	300 €	    50 €	    10	         50% / mois	      5%

Les animaux ne se reproduisent pas s’ils n’ont pas accès à un habitat suffisant pour eux et leur futur jeune.

Maladies
Taux de mortalité : 10%
Durée ±20% selon l’animal
| Espèce | Probabilité par an | Durée maladie    |
|--------|--------------------|------------------|
| Tigre  | 30%                | 15 jours         |
| Aigle  | 10%                | 30 jours         |
| Poule  | 5%                 | 5 jours          |

Les animaux malades ne peuvent pas se reproduire.

Visiteurs & Revenus
Espèce	Saison haute	Saison basse
Tigre	30 visiteurs / mois	5
Poule	2	0,5
Aigle	15	7

Saison haute : mai à septembre
Tarif : adulte 17 €, enfant 13 €
Topologie : 2 adultes + 2 enfants

Budget & Subventions
Budget initial : 80 000 €

Subvention annuelle :
Tigre : 43 800 € par individu
Aigle : 2 190 € par individu

Événements exceptionnels
Type	Probabilité mensuelle	Effet
Incendie	1%	Perte d’un habitat
Vol	1%	Perte d’un spécimen
Nuisibles	20%	Perte de 10% des graines
Viande avariée	10%	Perte de 20% de la viande


Le jeu fonctionne par menu à choix numérotés :

Acheter un animal
Vendre un animal
Acheter de la nourriture
Acheter un habitat
Vendre un habitat
Passer au mois suivant

Chaque action modifie le budget, la nourriture disponible, et peut déclencher des événements automatiques comme naissance, mortalité, maladie ou surpopulation.

Console de commande

Lancer le projet (github Public)
Cloner le dépôt : git clone https://github.com/Lilou-28/zoo-poo-c.git
Compiler et exécuter : dotnet run

Suivre les instructions du menu pour gérer votre zoo.
# zoo-poo-c

🦁 **README : Guide du Simulateur de Parc Animalier**

Ce document détaille les règles, statistiques et mécaniques économiques nécessaires à la gestion du parc. L'objectif est de maintenir l'équilibre entre le bien-être animal, la reproduction et la rentabilité financière.

---

## 🐾 1. Gestion des Animaux

Le parc accueille trois espèces avec des besoins et des cycles de vie distincts.

### Alimentation et Coûts

**Carnivores** (Viande : 5€/kg)
- Tigre Mâle : 12 kg/jour
- Tigre Femelle : 10 kg/jour
- Aigle Mâle : 0,25 kg/jour
- Aigle Femelle : 0,3 kg/jour

**Granivores** (Graines : 2,5€/kg)
- Poule : 0,15 kg/jour
- Coq : 0,18 kg/jour

⚠️ **Règles Critiques :**
- Les femelles en gestation consomment 2x plus de nourriture.
- Un animal affamé ne se reproduit pas.
- Une femelle affamée perd immédiatement son fœtus.

### Reproduction et Vie

| Espèce | Maturité Sexuelle | Gestation | Fin de Reprod. | Espérance de vie |
|--------|-------------------|-----------|----------------|------------------|
| Tigre  | 6 ans             | 3 mois    | 14 ans         | 25 ans           |
| Aigle  | 4 ans             | 45 jours  | 14 ans         | 25 ans           |
| Poule  | 6 mois            | 6 semaines| 8 ans          | 15 ans           |

Mortalité infantile :
- Tigre (33%)
- Aigle (50%)
- Poule (50%)

Contraintes :
- Pas de reproduction le 1er mois après l'achat
- Pas de reproduction en cas de maladie ou de manque d'espace

---

## 🏡 2. Habitats et Infrastructures

Chaque espèce doit loger dans un habitat dédié. La surpopulation entraîne des pénalités sévères.

| Habitat       | Achat   | Vente  | Capacité   | Risque Surpopulation     |
|---------------|---------|--------|------------|--------------------------|
| Enclos Tigre  | 2 000 € | 500 €  | 2 tigres   | -1 individu / mois       |
| Volière Aigle | 2 000 € | 500 €  | 4 aigles   | -1 individu / mois       |
| Poulailler    | 300 €   | 50 €   | 10 poules  | -4 individus / mois      |

---

## 💰 3. Économie du Parc

- **Budget Initial :** 80 000 €

### Subventions Annuelles (Aide d'État)
- Tigre : 43 800 € / individu
- Aigle : 2 190 € / individu

### Entrées Visiteurs
- L'affluence varie selon la saison (Saison Haute : Mai à Septembre).
- Tarif Adulte : 17 €
- Tarif Enfant : 13 €

**Attractivité (Visiteurs/mois/spécimen) :**
- Tigre : 30 (Haut) / 5 (Bas)
- Aigle : 15 (Haut) / 7 (Bas)
- Poule : 2 (Haut) / 0,5 (Bas)

---

## 🏥 4. Santé et Aléas

### Maladies
Chaque année, un animal a une probabilité d'être malade (Mortalité +10%, Reprod. stoppée).
- Tigre : 30% de probabilité (Dure 15 jours)
- Aigle : 10% de probabilité (Dure 30 jours)
- Poule : 5% de probabilité (Dure 5 jours)

### Événements Exceptionnels (Mensuels)
- Incendie (1%) : Perte de 1 habitat
- Vol (1%) : Perte de 1 spécimen
- Nuisibles (20%) : Perte de 10% du stock de graines
- Viande avariée (10%) : Perte de 20% du stock de viande

---

## 🎮 5. Actions Possibles (Par tour)

- Achat / Vente d'animaux : Gérer la population et le patrimoine
- Achat de nourriture : Anticiper les besoins mensuels
- Achat / Vente d'habitats : Éviter la surpopulation
- Passer au tour suivant : Calcul automatique des naissances, décès et revenus

---

## Console de commande

### Lancer le projet (github Public)

1. Cloner le dépôt :
	```
	git clone https://github.com/Lilou-28/zoo-poo-c.git
	```
2. Compiler et exécuter :
	```
	dotnet run
	```

Suivre les instructions du menu pour gérer votre zoo.
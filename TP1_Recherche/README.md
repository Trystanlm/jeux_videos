# jeux_videos
# Proposition d’idée – Jeu de recherche à zones multiples

## Concept général

Je veux créer un jeu en 3D où le joueur explore plusieurs zones successives.
Dans chaque zone, il doit trouver un certain nombre d’objets pour débloquer la zone suivante.

Le principe est simple :

* Explorer
* Chercher des objets cachés
* Compléter un objectif
* Débloquer une nouvelle zone

Le jeu progresse par étapes, avec une difficulté qui augmente progressivement.

---

## Structure principale du jeu

### Système de progression

* Zone 1 → Trouver X objets (ex: 5)
* Une fois tous les objets trouvés → Déblocage d’un accès (porte, portail, passage)
* Zone 2 → Trouver moins d’objets mais plus difficiles d’accès
* Zone 3 → Objectif final (objet spécial ou combinaison d’objets)

Chaque zone est fermée au début et se débloque seulement si l’objectif précédent est complété.

---

## Gameplay principal

### Le joueur

* Personnage 3D contrôlable
* Animations : marche, course, idle, interaction
* Caméra en 3e personne

### Recherche d’objets

* Les objets sont dispersés dans l’environnement
* Certains peuvent être cachés ou protégés
* Interaction simple (touche pour ramasser)

---

## Systèmes obligatoires intégrés

### 1. Inventaire visuel

* Affichage des objets collectés à l’écran
* Compteur (ex: 3/5 objets)
* Mise à jour en temps réel

### 2. Mini-map (vue du haut)

* Caméra placée au-dessus
* Indique les endroits importants
* Certaines zones peuvent apparaître plus tard

### 3. Navigation Mesh

* Un ou plusieurs éléments qui se déplacent automatiquement

  * Ennemi qui patrouille
  * PNJ
* Utilisation du NavMesh pour les déplacements

### 4. Animations supplémentaires (Lerp)

* Porte qui s’ouvre progressivement
* Plateforme qui monte ou descend
* Objet qui flotte ou tourne

---

## Environnements

Chaque zone aura :

* Des bâtiments ou structures
* Des objets créés dans Blender
* Des éléments interactifs

On peut créer :

* Coffres
* Clés
* Cristaux
* Structures simples

---

## Objectif final du jeu

Après avoir complété toutes les zones :

* Déblocage d’un objectif final
* Message de victoire
* Fin de la partie

---


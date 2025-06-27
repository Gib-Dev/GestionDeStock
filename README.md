# 🏪 Gestion des Stocks - Stock-Ges

Une application moderne de gestion des stocks développée en WPF avec Material Design.

## ✨ Fonctionnalités

### 🔐 Authentification Sécurisée
- **Système d'authentification robuste** avec BCrypt
- **Gestion des rôles** (Admin, Manager, User)
- **Réinitialisation de mot de passe** sécurisée
- **Sessions utilisateur** avec tokens

### 📦 Gestion des Produits
- **CRUD complet** des produits
- **Recherche avancée** par nom, description, SKU, code-barres
- **Gestion des catégories** avec couleurs personnalisées
- **Suivi des stocks** en temps réel
- **Alertes de stock faible** automatiques
- **Calcul de la valeur totale** des stocks

### 📊 Tableau de Bord
- **Statistiques en temps réel**
- **Graphiques et métriques** clés
- **Alertes et notifications**
- **Vue d'ensemble** des opérations

### 🔄 Mouvements de Stock
- **Entrées et sorties** de stock
- **Ajustements** et retours
- **Historique complet** des mouvements
- **Traçabilité** des opérations

### 📈 Rapports
- **Rapports de stock** détaillés
- **Analyses par catégorie**
- **Export de données**
- **Statistiques** avancées

## 🛠️ Technologies Utilisées

- **.NET 8.0** - Framework principal
- **WPF** - Interface utilisateur
- **Material Design** - Design moderne
- **Entity Framework Core** - ORM
- **SQLite** - Base de données
- **BCrypt** - Hachage sécurisé
- **Dependency Injection** - Architecture modulaire

## 🚀 Installation et Configuration

### Prérequis
- Visual Studio 2022 ou plus récent
- .NET 8.0 SDK
- Windows 10/11

### Installation
1. **Cloner le repository**
   ```bash
   git clone https://github.com/Gib-Dev/GestionDeStock.git
   cd GESTION-DES-STOCKS
   ```

2. **Restaurer les packages NuGet**
   ```bash
   dotnet restore
   ```

3. **Compiler le projet**
   ```bash
   dotnet build
   ```

4. **Lancer l'application**
   ```bash
   dotnet run
   ```

### Configuration de la Base de Données
La base de données SQLite sera créée automatiquement au premier lancement avec :
- **Utilisateur admin par défaut** : `admin` / `admin123`
- **Catégories pré-configurées** : Électronique, Vêtements, Livres, Alimentation
- **Produits d'exemple** pour tester l'application

## 📁 Structure du Projet

```
GESTION-DES-STOCKS/
├── Models/                 # Modèles de données
│   ├── User.cs            # Utilisateur et authentification
│   ├── Product.cs         # Produits
│   ├── Category.cs        # Catégories
│   └── StockMovement.cs   # Mouvements de stock
├── Data/                  # Couche de données
│   └── StockDbContext.cs  # Contexte Entity Framework
├── Services/              # Services métier
│   ├── AuthService.cs     # Service d'authentification
│   ├── ProductService.cs  # Service des produits
│   └── CategoryService.cs # Service des catégories
├── Views/                 # Vues principales
│   └── Homepage.xaml      # Page d'accueil
├── Resources/             # Ressources
│   ├── Images/           # Images et icônes
│   └── Styles.xaml       # Styles personnalisés
└── UserControls/         # Contrôles utilisateur
    ├── ProductsControl.xaml
    ├── CategoriesControl.xaml
    ├── DashboardControl.xaml
    └── ...
```

## 🔧 Améliorations Apportées

### ✅ Authentification Sécurisée
- **Remplacement** de l'authentification basique (admin/admin)
- **Implémentation** de BCrypt pour le hachage des mots de passe
- **Gestion des rôles** et permissions
- **Système de tokens** pour les sessions

### ✅ Base de Données Robuste
- **Migration** des données en mémoire vers SQLite
- **Entity Framework Core** pour l'ORM
- **Relations** entre entités (Produits ↔ Catégories)
- **Index** pour les performances
- **Soft delete** pour la conservation des données

### ✅ Architecture Moderne
- **Pattern Repository** avec services
- **Dependency Injection** pour la modularité
- **Validation** des données avec Data Annotations
- **Gestion d'erreurs** robuste
- **Logging** des opérations

### ✅ Interface Améliorée
- **Design responsive** et moderne
- **Feedback utilisateur** amélioré
- **Validation** en temps réel
- **Recherche** avancée
- **Notifications** et alertes

### ✅ Fonctionnalités Avancées
- **Gestion des mouvements** de stock
- **Calculs automatiques** (valeur totale, stock faible)
- **Historique** des opérations
- **Export** et rapports
- **Paramètres** utilisateur

## 🔐 Sécurité

### Authentification
- **Mots de passe hachés** avec BCrypt
- **Validation** des entrées utilisateur
- **Protection** contre les injections SQL
- **Sessions sécurisées**

### Données
- **Validation** côté serveur et client
- **Encodage** des données sensibles
- **Backup** automatique recommandé
- **Audit trail** des modifications

## 📊 Utilisation

### Connexion
1. Lancez l'application
2. Utilisez les identifiants par défaut : `admin` / `admin123`
3. Ou créez un nouveau compte

### Gestion des Produits
1. Accédez à "Gestion des Produits"
2. Ajoutez, modifiez ou supprimez des produits
3. Utilisez la recherche pour trouver rapidement
4. Surveillez les alertes de stock faible

### Gestion des Catégories
1. Accédez à "Catégories"
2. Organisez vos produits par catégories
3. Personnalisez les couleurs
4. Visualisez les statistiques par catégorie

### Tableau de Bord
1. Consultez les **métriques clés**
2. Surveillez les **alertes**
3. Analysez les **tendances**
4. Accédez aux **rapports**

## 🐛 Dépannage

### Problèmes Courants

**Erreur de base de données**
- Vérifiez que SQLite est installé
- Supprimez le fichier `StockManagement.db` pour recréer la base

**Problème d'authentification**
- Utilisez les identifiants par défaut : `admin` / `admin123`
- Vérifiez la console de débogage pour les mots de passe temporaires

**Performance lente**
- Vérifiez l'espace disque disponible
- Optimisez les requêtes si nécessaire

## 🔮 Évolutions Futures

- [ ] **Interface web** responsive
- [ ] **API REST** pour intégrations
- [ ] **Synchronisation cloud**
- [ ] **Codes-barres** et QR codes
- [ ] **Notifications push**
- [ ] **Multi-utilisateurs** en temps réel
- [ ] **Mobile app** (Xamarin/MAUI)
- [ ] **Intelligence artificielle** pour les prévisions

## 📝 Licence

Ce projet est sous licence MIT. Voir le fichier `LICENSE` pour plus de détails.

## 🤝 Contribution

Les contributions sont les bienvenues ! N'hésitez pas à :
1. Fork le projet
2. Créer une branche pour votre fonctionnalité
3. Commiter vos changements
4. Pousser vers la branche
5. Ouvrir une Pull Request

## 📞 Support

Pour toute question ou problème :
- Ouvrez une issue sur GitHub
- Consultez la documentation
- Contactez l'équipe de développement

---

**Développé avec ❤️ pour une gestion des stocks moderne et efficace** 


SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `adsl`
--

-- --------------------------------------------------------

--
-- Structure de la table `client`
--

CREATE TABLE `client` (
  `id_client` int(10) UNSIGNED NOT NULL,
  `nom_prenom` varchar(45) NOT NULL,
  `adresse` varchar(45) NOT NULL,
  `date_naissance` date NOT NULL,
  `cin` varchar(45) NOT NULL,
  `date_cin` date NOT NULL,
  `numero_gsm` varchar(45) NOT NULL,
  `numero_fixe` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `client`
--

INSERT INTO `client` (`id_client`, `nom_prenom`, `adresse`, `date_naissance`, `cin`, `date_cin`, `numero_gsm`, `numero_fixe`) VALUES
(1, 'youssef mami', 'avenue habib bourguiba', '1986-06-05', '06411250', '2006-11-11', '22336699', '22336699'),
(4, 'jawher khelil', 'qsdfgh', '2017-07-19', '19/07/2017 00:00:00', '2017-07-19', '123456', '852147'),
(5, 'foulen fouleni', 'azer', '2016-04-26', '', '2017-06-28', '78963252', '52896325');

-- --------------------------------------------------------

--
-- Structure de la table `contrat`
--

CREATE TABLE `contrat` (
  `id_contrat` int(11) NOT NULL,
  `id_client` int(11) NOT NULL,
  `id_tarif` int(11) NOT NULL,
  `modalite` int(11) NOT NULL,
  `date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `contrat`
--

INSERT INTO `contrat` (`id_contrat`, `id_client`, `id_tarif`, `modalite`, `date`) VALUES
(1, 4, 10, 2, '2017-07-04'),
(2, 0, 0, 1, '2017-07-22'),
(3, 0, 0, 6, '2017-07-13'),
(4, 0, 0, 3, '2017-07-12'),
(5, 0, 0, 0, '2017-07-17'),
(6, 0, 0, 0, '2017-01-01'),
(7, 1, 1, 1, '2017-07-22'),
(8, 1, 1, 12, '2017-07-04'),
(9, 1, 2, 3, '2017-07-04'),
(10, 5, 6, 3, '2017-07-29'),
(14, 5, 5, 12, '2017-10-24'),
(16, 4, 5, 6, '2017-10-12');

-- --------------------------------------------------------

--
-- Structure de la table `operation`
--

CREATE TABLE `operation` (
  `id_operation` int(11) NOT NULL,
  `id_client` int(11) NOT NULL,
  `id_contrat` int(11) NOT NULL,
  `somme` varchar(10) NOT NULL,
  `datepaiement` datetime NOT NULL,
  `num` int(11) NOT NULL,
  `nom_prenom` varchar(100) NOT NULL,
  `type` varchar(100) NOT NULL,
  `modalite` varchar(10) NOT NULL,
  `source` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `operation`
--

INSERT INTO `operation` (`id_operation`, `id_client`, `id_contrat`, `somme`, `datepaiement`, `num`, `nom_prenom`, `type`, `modalite`, `source`) VALUES
(29, 0, 0, '', '2017-10-29 21:52:44', 0, '', '', '', 'jawher khe'),
(30, 0, 14, '180', '2017-10-29 21:52:55', 0, 'foulen fouleni', 'ADSL 8 mo ', '12', 'jawher khe'),
(31, 0, 9, '45', '2017-10-29 22:11:59', 0, 'youssef mami', 'ADSL 4 mo ', '3', 'jawher khe'),
(32, 0, 14, '180', '2017-10-29 22:20:04', 0, 'foulen fouleni', 'ADSL 8 mo ', '12', 'jawher khe');

-- --------------------------------------------------------

--
-- Structure de la table `tarif`
--

CREATE TABLE `tarif` (
  `id_tarif` int(10) UNSIGNED NOT NULL,
  `nom_produit` varchar(45) NOT NULL,
  `duree` varchar(45) NOT NULL,
  `prix` varchar(45) NOT NULL,
  `tva` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `tarif`
--

INSERT INTO `tarif` (`id_tarif`, `nom_produit`, `duree`, `prix`, `tva`) VALUES
(2, 'ADSL 4 mo ', '1', '15', '0'),
(5, 'ADSL 8 mo ', '2', '15', '20');

-- --------------------------------------------------------

--
-- Structure de la table `user`
--

CREATE TABLE `user` (
  `id_user` int(10) UNSIGNED NOT NULL,
  `login` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `nom_prenom` varchar(45) NOT NULL,
  `type` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `user`
--

INSERT INTO `user` (`id_user`, `login`, `password`, `nom_prenom`, `type`) VALUES
(1, 'admin', 'admin', 'jawher khelil', 'administrateur'),
(5, 'user', 'user', 'azerty', 'administrateur'),
(9, 'teste', 'teste', 'test7', 'technicien');

--
-- Index pour les tables exportées
--

--
-- Index pour la table `client`
--
ALTER TABLE `client`
  ADD PRIMARY KEY (`id_client`);

--
-- Index pour la table `contrat`
--
ALTER TABLE `contrat`
  ADD PRIMARY KEY (`id_contrat`);

--
-- Index pour la table `operation`
--
ALTER TABLE `operation`
  ADD PRIMARY KEY (`id_operation`);

--
-- Index pour la table `tarif`
--
ALTER TABLE `tarif`
  ADD PRIMARY KEY (`id_tarif`);

--
-- Index pour la table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id_user`);

--
-- AUTO_INCREMENT pour les tables exportées
--

--
-- AUTO_INCREMENT pour la table `client`
--
ALTER TABLE `client`
  MODIFY `id_client` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT pour la table `contrat`
--
ALTER TABLE `contrat`
  MODIFY `id_contrat` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;
--
-- AUTO_INCREMENT pour la table `operation`
--
ALTER TABLE `operation`
  MODIFY `id_operation` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=33;
--
-- AUTO_INCREMENT pour la table `tarif`
--
ALTER TABLE `tarif`
  MODIFY `id_tarif` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT pour la table `user`
--
ALTER TABLE `user`
  MODIFY `id_user` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

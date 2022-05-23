-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Tempo de geração: 22-Maio-2022 às 17:29
-- Versão do servidor: 5.7.34
-- versão do PHP: 7.4.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `pap_fabio`
--
CREATE DATABASE IF NOT EXISTS `pap_fabio` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `pap_fabio`;

-- --------------------------------------------------------

--
-- Estrutura da tabela `utilizadores`
--

DROP TABLE IF EXISTS `utilizadores`;
CREATE TABLE `utilizadores` (
  `id_user` int(11) NOT NULL,
  `email` varchar(50) NOT NULL,
  `pass` varchar(200) NOT NULL,
  `nome` varchar(50) NOT NULL,
  `admin` tinyint(1) NOT NULL COMMENT 'Admin - 1\r\nNão/Admin - 0',
  `tipo` int(1) NOT NULL COMMENT 'Professor -1\r\nFuncionário - 2\r\nAluno - 3\r\n',
  `codigoAluno` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `utilizadores`
--

INSERT INTO `utilizadores` (`id_user`, `email`, `pass`, `nome`, `admin`, `tipo`, `codigoAluno`) VALUES
(1, 'jfmmonteiro@gmail.com', 'AQAAAAEAACcQAAAAEPVRLIys24pi04S2A6ob77g1ifScAkUVemWq1Wot6AAX1iZShxfIvrcEvy7nWTcBWA==', 'Jorge Monteiro', 1, 3, 'a12345');

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `utilizadores`
--
ALTER TABLE `utilizadores`
  ADD PRIMARY KEY (`id_user`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `utilizadores`
--
ALTER TABLE `utilizadores`
  MODIFY `id_user` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

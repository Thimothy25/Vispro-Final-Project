-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 14, 2024 at 03:04 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_playpad`
--

-- --------------------------------------------------------

--
-- Table structure for table `lapangan1`
--

CREATE TABLE `lapangan1` (
  `id_booking` int(11) NOT NULL,
  `nama` varchar(20) NOT NULL,
  `no_tlp` int(15) NOT NULL,
  `tanggal` varchar(100) NOT NULL,
  `jam_mulai` time NOT NULL,
  `jam_selesai` time NOT NULL,
  `status` enum('Available','Booked') NOT NULL DEFAULT 'Booked',
  `biaya` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `lapangan1`
--

INSERT INTO `lapangan1` (`id_booking`, `nama`, `no_tlp`, `tanggal`, `jam_mulai`, `jam_selesai`, `status`, `biaya`) VALUES
(36, 'moty', 822485678, 'Thursday, 14 November 2024', '08:00:00', '09:00:00', 'Booked', 40000.00);

-- --------------------------------------------------------

--
-- Table structure for table `lapangan2`
--

CREATE TABLE `lapangan2` (
  `id_booking` int(11) NOT NULL,
  `nama` varchar(20) NOT NULL,
  `no_tlp` int(15) NOT NULL,
  `tanggal` varchar(100) NOT NULL,
  `jam_mulai` time NOT NULL,
  `jam_selesai` time NOT NULL,
  `status` enum('Available','Booked') NOT NULL DEFAULT 'Booked',
  `biaya` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `lapangan3`
--

CREATE TABLE `lapangan3` (
  `id_booking` int(11) NOT NULL,
  `nama` varchar(20) NOT NULL,
  `no_tlp` int(15) NOT NULL,
  `tanggal` varchar(100) NOT NULL,
  `jam_mulai` time NOT NULL,
  `jam_selesai` time NOT NULL,
  `status` enum('Available','Booked') NOT NULL DEFAULT 'Booked',
  `biaya` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tbl_pengguna`
--

CREATE TABLE `tbl_pengguna` (
  `id_pengguna` int(11) NOT NULL,
  `username` varchar(20) NOT NULL,
  `password` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_pengguna`
--

INSERT INTO `tbl_pengguna` (`id_pengguna`, `username`, `password`) VALUES
(5, 'Reymar', 'pongantung'),
(6, 'adika', '12345678'),
(7, 'admin', 'admin');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `lapangan1`
--
ALTER TABLE `lapangan1`
  ADD PRIMARY KEY (`id_booking`);

--
-- Indexes for table `lapangan2`
--
ALTER TABLE `lapangan2`
  ADD PRIMARY KEY (`id_booking`);

--
-- Indexes for table `lapangan3`
--
ALTER TABLE `lapangan3`
  ADD PRIMARY KEY (`id_booking`);

--
-- Indexes for table `tbl_pengguna`
--
ALTER TABLE `tbl_pengguna`
  ADD PRIMARY KEY (`id_pengguna`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `lapangan1`
--
ALTER TABLE `lapangan1`
  MODIFY `id_booking` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=38;

--
-- AUTO_INCREMENT for table `lapangan2`
--
ALTER TABLE `lapangan2`
  MODIFY `id_booking` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `lapangan3`
--
ALTER TABLE `lapangan3`
  MODIFY `id_booking` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `tbl_pengguna`
--
ALTER TABLE `tbl_pengguna`
  MODIFY `id_pengguna` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

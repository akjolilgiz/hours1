-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Oct 04, 2018 at 01:34 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `clock_management`
--
CREATE DATABASE IF NOT EXISTS `clock_management` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `clock_management`;

-- --------------------------------------------------------

--
-- Table structure for table `departments`
--

CREATE TABLE `departments` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `departments`
--

INSERT INTO `departments` (`id`, `name`) VALUES
(1, 'Marketingll'),
(2, 'Operations'),
(3, 'Finance'),
(4, 'HR');

-- --------------------------------------------------------

--
-- Table structure for table `departments_employees`
--

CREATE TABLE `departments_employees` (
  `id` int(11) NOT NULL,
  `department_id` int(11) NOT NULL,
  `employee_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;

--
-- Dumping data for table `departments_employees`
--

INSERT INTO `departments_employees` (`id`, `department_id`, `employee_id`) VALUES
(1, 0, 0),
(2, 1, 8),
(3, 1, 9),
(4, 0, 0),
(5, 0, 3),
(6, 0, 10),
(7, 0, 11),
(8, 0, 12),
(9, 2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `employees`
--

CREATE TABLE `employees` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;

--
-- Dumping data for table `employees`
--

INSERT INTO `employees` (`id`, `name`, `username`, `password`) VALUES
(1, 'Mel', 'iris', 'iris'),
(2, 'Chan', 'chan', 'chan'),
(3, 'Chan', 'chan', 'chan'),
(4, 'George', 'George', 'George'),
(5, 'Chan', 'George', 'iris'),
(6, 'Mel', 'George', 'George'),
(7, 'Mel', 'George', 'George'),
(8, 'Mel', 'George', 'George'),
(9, 'Conor2', 'conor', 'conor'),
(10, 'Mel Gibson', 'mel', 'gibson'),
(11, 'John', 'john', 'travolta'),
(12, 'Connor', 'mcgregor', 'mcgregor');

-- --------------------------------------------------------

--
-- Table structure for table `employees_hours`
--

CREATE TABLE `employees_hours` (
  `id` int(11) NOT NULL,
  `clock_in` datetime NOT NULL,
  `clock_out` datetime NOT NULL,
  `hours` time NOT NULL,
  `employee_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `employees_hours`
--

INSERT INTO `employees_hours` (`id`, `clock_in`, `clock_out`, `hours`, `employee_id`) VALUES
(109, '2018-10-03 16:21:59', '0000-00-00 00:00:00', '00:00:00', 1),
(110, '2018-10-03 16:24:06', '0000-00-00 00:00:00', '00:00:00', 1),
(111, '2018-10-03 16:24:07', '0000-00-00 00:00:00', '00:00:00', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `departments`
--
ALTER TABLE `departments`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `departments_employees`
--
ALTER TABLE `departments_employees`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `employees`
--
ALTER TABLE `employees`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `employees_hours`
--
ALTER TABLE `employees_hours`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `departments_employees`
--
ALTER TABLE `departments_employees`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
--
-- AUTO_INCREMENT for table `employees`
--
ALTER TABLE `employees`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
--
-- AUTO_INCREMENT for table `employees_hours`
--
ALTER TABLE `employees_hours`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=112;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

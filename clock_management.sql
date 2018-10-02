-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Oct 02, 2018 at 11:58 PM
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
(1, 'Marketing'),
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

-- --------------------------------------------------------

--
-- Table structure for table `employees`
--

CREATE TABLE `employees` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `employees`
--

INSERT INTO `employees` (`id`, `name`, `username`, `password`) VALUES
(1, 'Mel Yasuda', '', ''),
(2, 'Akjol Jaenbai', '', ''),
(3, 'Chan Lee', '', ''),
(4, 'Connor McCarthy', '', ''),
(5, 'Ahmed Khokar', '', ''),
(6, 'Chris Crow', '', ''),
(7, 'David Mortkowitz', '', ''),
(8, 'Hyewon Cho', '', ''),
(9, 'Hyung Lee', '', ''),
(10, 'Julius Bade', '', ''),
(11, 'Kenneth Du', '', ''),
(12, 'David Zhu', '', ''),
(13, 'Brian Nelson', '', ''),
(14, 'Mark Mangahas', '', ''),
(15, 'Panatada Catlin', '', ''),
(16, 'Vera Weikel', '', ''),
(17, 'Regina Nurieva', '', ''),
(18, 'Skye Nguyen', '', ''),
(19, 'Derek Smith', '', ''),
(20, 'Catherine Bradley', '', ''),
(21, 'Victoria Oh', '', ''),
(5, '', 'username', 'password'),
(0, 'AJ', 'user', 'name'),
(23, 'AJ', 'user', 'name');

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
(80, '2018-10-02 13:00:55', '2018-10-02 13:25:34', '23:35:21', 3),
(81, '2018-10-02 13:00:55', '2018-10-02 13:25:34', '23:35:21', 3),
(82, '2018-10-02 13:00:55', '2018-10-02 13:25:34', '23:35:21', 3),
(83, '2018-10-02 13:00:55', '2018-10-02 13:25:34', '23:35:21', 3),
(84, '2018-10-02 13:00:55', '2018-10-02 13:25:34', '23:35:21', 3),
(85, '2018-10-02 14:39:34', '2018-10-02 14:39:40', '-00:00:06', 1),
(86, '2018-10-02 14:39:34', '2018-10-02 14:39:40', '-00:00:06', 1),
(87, '2018-10-02 14:39:34', '2018-10-02 14:39:40', '-00:00:06', 1);

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
-- Indexes for table `employees_hours`
--
ALTER TABLE `employees_hours`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `employees_hours`
--
ALTER TABLE `employees_hours`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=88;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

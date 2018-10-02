-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Sep 30, 2018 at 03:02 AM
-- Server version: 5.7.23
-- PHP Version: 7.2.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

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
-- Table structure for table `departments_employeess`
--

CREATE TABLE `departments_employeess` (
  `id` int(11) NOT NULL,
  `department_id` int(11) NOT NULL,
  `employee_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `employees`
--

CREATE TABLE `employees` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `employees`
--

INSERT INTO `employees` (`id`, `name`) VALUES
(1, 'Mel Yasuda'),
(2, 'Akjol Jaenbai'),
(3, 'Chan Lee'),
(4, 'Connor McCarthy'),
(5, 'Ahmed Khokar'),
(6, 'Chris Crow'),
(7, 'David Mortkowitz'),
(8, 'Hyewon Cho'),
(9, 'Hyung Lee'),
(10, 'Julius Bade'),
(11, 'Kenneth Du'),
(12, 'David Zhu'),
(13, 'Brian Nelson'),
(14, 'Mark Mangahas'),
(15, 'Panatada Catlin'),
(16, 'Vera Weikel'),
(17, 'Regina Nurieva'),
(18, 'Skye Nguyen'),
(19, 'Derek Smith'),
(20, 'Catherine Bradley'),
(21, 'Victoria Oh');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `departments`
--
ALTER TABLE `departments`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `departments_employeess`
--
ALTER TABLE `departments_employeess`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `employees`
--
ALTER TABLE `employeess`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `departments`
--
ALTER TABLE `departments`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `departments_employeess`
--
ALTER TABLE `departments_employees`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `employees`
--
ALTER TABLE `employees`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

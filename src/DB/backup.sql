-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: netcore.lamp
-- ------------------------------------------------------
-- Server version	5.6.26

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `programforweb`
--

DROP TABLE IF EXISTS `programforweb`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `programforweb` (
  `Id` int(11) NOT NULL,
  `Description` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `AreaName` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `ControllerName` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `ActionName` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `Method` int(11) DEFAULT NULL,
  `Params` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `ModuleName` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `InTime` datetime DEFAULT NULL,
  `ModifyUser` char(36) DEFAULT NULL,
  `InUse` bit(1) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `programforweb`
--

LOCK TABLES `programforweb` WRITE;
/*!40000 ALTER TABLE `programforweb` DISABLE KEYS */;
/*!40000 ALTER TABLE `programforweb` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role` (
  `Id` char(36) NOT NULL,
  `ParentId` char(36) DEFAULT NULL,
  `Name` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `Description` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `InUse` bit(1) DEFAULT NULL,
  `InTime` datetime DEFAULT NULL,
  `ModifyTime` datetime DEFAULT NULL,
  `ModifyUser` char(36) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `Id` char(36) NOT NULL,
  `UserName` varchar(50) CHARACTER SET utf8 NOT NULL,
  `Password` varchar(50) CHARACTER SET utf8 NOT NULL,
  `RealName` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `PhoneNo` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `Email` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `InUse` bit(1) DEFAULT NULL,
  `InTime` datetime DEFAULT NULL,
  `ModifyTime` datetime DEFAULT NULL,
  `ModifyUser` char(36) DEFAULT NULL,
  `LoginTime` datetime DEFAULT NULL,
  `LoginTimes` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userroleclaim`
--

DROP TABLE IF EXISTS `userroleclaim`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `userroleclaim` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` char(36) DEFAULT NULL,
  `RoleId` char(36) DEFAULT NULL,
  `InUse` bit(1) DEFAULT NULL,
  `Intime` datetime DEFAULT NULL,
  `ModifyTime` datetime DEFAULT NULL,
  `ModifyUser` char(36) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userroleclaim`
--

LOCK TABLES `userroleclaim` WRITE;
/*!40000 ALTER TABLE `userroleclaim` DISABLE KEYS */;
/*!40000 ALTER TABLE `userroleclaim` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-08-31 20:02:08

/*
 Navicat Premium Data Transfer

 Source Server         : ChuanGoing
 Source Server Type    : MySQL
 Source Server Version : 50719
 Source Host           : localhost:3306
 Source Schema         : ChuanGoing

 Target Server Type    : MySQL
 Target Server Version : 50719
 File Encoding         : 65001

 Date: 18/11/2019 15:51:35
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for Nlog
-- ----------------------------
DROP TABLE IF EXISTS `Nlog`;
CREATE TABLE `Nlog`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Application` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Date` datetime(0) NOT NULL,
  `Level` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Message` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Logger` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Callsite` varchar(512) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Exception` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 10004 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;

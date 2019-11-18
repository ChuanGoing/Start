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

 Date: 18/11/2019 15:54:22
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for Order
-- ----------------------------
DROP TABLE IF EXISTS `Order`;
CREATE TABLE `Order`  (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Sn` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '流水号',
  `TotalPrice` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '总价',
  `UserId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0' COMMENT '用户编号',
  `Adress` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '地址',
  `PaymentTime` bigint(20) NOT NULL DEFAULT 0 COMMENT '支付时间',
  `ExpireTime` bigint(20) NOT NULL DEFAULT 0 COMMENT '过期时间',
  `Status` int(11) NOT NULL COMMENT '订单状态',
  `Description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '' COMMENT '备注',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;

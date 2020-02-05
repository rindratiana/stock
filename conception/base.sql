/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     28/01/2020 17:04:31                          */
/*==============================================================*/


drop table if exists COMMANDE;

drop table if exists COMPTOIR;

drop table if exists DETAIL_COMMANDE;

drop table if exists DUREE;

drop table if exists POSTE;

drop table if exists SORTIE;

drop table if exists UTILISATEUR;

/*==============================================================*/
/* Table: COMMANDE                                              */
/*==============================================================*/
create table COMMANDE
(
   ID_COMMANDE          int not null auto_increment,
   DATE_COMMANDE        date,
   NUMERO               varchar(20),
   ID_COMPTOIR          int not null,
   CLIENT               int,
   ETAT                 varchar(7),
   primary key (ID_COMMANDE)
);

/*==============================================================*/
/* Table: COMPTOIR                                              */
/*==============================================================*/
create table COMPTOIR
(
   ID_COMPTOIR          int not null auto_increment,
   NOM_COMPTOIR         int,
   primary key (ID_COMPTOIR)
);

/*==============================================================*/
/* Table: DETAIL_COMMANDE                                       */
/*==============================================================*/
create table DETAIL_COMMANDE
(
   ID_DETAIL_COMMANDE int not null auto_increment primary key,
   ID_COMMANDE          int not null,
   REFERENCE_ARTICLE    varchar(20),
   QUANTITE             int,
   EMPLACEMENT          int
);

/*==============================================================*/
/* Table: DUREE                                                 */
/*==============================================================*/
create table DUREE
(
   ID_COMMANDE          int not null,
   HEURE_COMMANDE       datetime,
   HEURE_SORTIE         datetime,
   HEURE_LIVRAISON      datetime
);

/*==============================================================*/
/* Table: POSTE                                                 */
/*==============================================================*/
create table POSTE
(
   ID_POSTE             int not null auto_increment,
   NOM_POSTE            varchar(50),
   primary key (ID_POSTE)
);

/*==============================================================*/
/* Table: SORTIE                                                */
/*==============================================================*/
create table SORTIE
(
   ID_SORTIE            int not null auto_increment,
   ID_COMMANDE          int not null,
   ID_BINOME            int not null,
   ID_MAGASINIER        int not null,
   primary key (ID_SORTIE)
);

/*==============================================================*/
/* Table: UTILISATEUR                                           */
/*==============================================================*/
create table UTILISATEUR
(
   ID_UTILISATEUR       int not null auto_increment,
   ID_POSTE             int not null,
   NOM_UTILISATEUR      varchar(50),
   PRENOMS              varchar(50),
   IDENTIFIANTS         varchar(50),
   MDP                  varchar(50),
   primary key (ID_UTILISATEUR)
);

alter table COMMANDE add constraint FK_COMMANDE_CAISSIER foreign key (ID_AUXILIAIRE_VENTE)
      references UTILISATEUR (ID_UTILISATEUR) on delete restrict on update restrict;

alter table COMMANDE add constraint FK_COMMANDE_COMPTOIR foreign key (ID_COMPTOIR)
      references COMPTOIR (ID_COMPTOIR) on delete restrict on update restrict;

alter table DETAIL_COMMANDE add constraint FK_RELATIONSHIP_8 foreign key (ID_COMMANDE)
      references COMMANDE (ID_COMMANDE) on delete restrict on update restrict;

alter table DUREE add constraint FK_DUREE_COMMANDE foreign key (ID_COMMANDE)
      references COMMANDE (ID_COMMANDE) on delete restrict on update restrict;

alter table SORTIE add constraint FK_BINOME_SORTIE foreign key (ID_BINOME)
      references UTILISATEUR (ID_UTILISATEUR) on delete restrict on update restrict;

alter table SORTIE add constraint FK_COMMANDE_SORTIE foreign key (ID_COMMANDE)
      references COMMANDE (ID_COMMANDE) on delete restrict on update restrict;

alter table SORTIE add constraint FK_MAGASINIER_SORTIE foreign key (ID_MAGASINIER)
      references UTILISATEUR (ID_UTILISATEUR) on delete restrict on update restrict;

alter table UTILISATEUR add constraint FK_POSTE_UTILISATEUR foreign key (ID_POSTE)
      references POSTE (ID_POSTE) on delete restrict on update restrict;


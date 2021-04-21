# PGPGenerator
Use this to generate PGP 
===================================HELP===================================
Summary
1.Use This to generate PGP KEY.
2.You can run this program with parameter or not. If you run it without parameter,you must input username and password that is used for generating PGP Key
3.Parameter for username is 'U' and parameter for password is 'P'.

How to use
1.Change target directory in app.config to choose the location for generating key. (TargetPublicDir for public key, TargetPrivateDir for private key,TargetUserPassDir for username and encrypted password directory)
2.Run this program. You can use parameter or not. 
3.Username and password in this is not synchronize with Active Directory.
4.After done generating, move public key and private key to WSSMS Location.
5.Save it to Framework Database the location of PrivateKey and PublicKey.
6.Save the Username and password encrypted to into Framework Database.

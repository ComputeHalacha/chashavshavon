from google.appengine.ext import db
from google.appengine.ext.db import GqlQuery

class Users(db.Model):
    userName = db.StringProperty(required=True)
    password = db.StringProperty(required=True)
    isAdmin = db.BooleanProperty()

class Files(db.Model):
    user = db.ReferenceProperty(Users, collection_name='files_set', required=True)
    fileName = db.StringProperty(required=True)
    fileText = db.TextProperty()

def GetUser(userName, password):
    return Users.gql('WHERE userName = :1 AND password = :2',
                     userName,
                     password).get()

def GetAdminUserKey(userName, password):
    return GqlQuery('SELECT __key__ FROM Users WHERE userName = :1 AND password = :2 AND isAdmin = True',
                       userName,
                       password).get()

def GetAdminUser(ekey):
    return Users.gql('WHERE __key__ = :1 AND isAdmin = True',
                       db.Key(ekey)).get()

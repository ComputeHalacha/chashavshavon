import webapp2
from google.appengine.ext import db
from google.appengine.ext.db import GqlQuery
import datamodule
import htmlHelper

class NewUser(webapp2.RequestHandler):
    def get(self):
        self.newUser()

    def post(self):
        self.newUser()

    def newUser(self):
        htmlHelper.SetContentTypeToXml(self)
        userName = self.request.get('userName').strip()
        password = self.request.get('password').strip()
        isAdminUser = self.request.get('isAdmin')

        if(len(userName) < 2):
           self.response.out.write(htmlHelper.XmlDeclaration + '<error errorId="1">Invalid username</error>')
           return
        if(len(password) < 4):
           self.response.out.write(htmlHelper.XmlDeclaration + '<error errorId="2">Invalid password</error>')
           return
        if(datamodule.GetUser(userName, password)):
           self.response.out.write(htmlHelper.XmlDeclaration + '<error errorId="3">Username and password already taken</error>')
           return

        user = datamodule.Users(userName = userName, password = password, isAdmin = (True if isAdminUser else False))
        user.put()
        self.response.out.write(htmlHelper.XmlDeclaration + '<OK>User Created</OK>')

class DeleteUser(webapp2.RequestHandler):
  def get(self):
      self.deleteUser()

  def post(self):
      self.deleteUser()

  def deleteUser(self):
      htmlHelper.SetContentTypeToXml(self)
      user = datamodule.GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not user):
          self.response.out.write(htmlHelper.XmlDeclaration + '<error errorId="4">User not found</error>')
          return

      files = GqlQuery('SELECT __key__ FROM Files WHERE user = :1', user)
      db.delete(files.fetch(1000, 0))
      user.delete()
      self.response.out.write(htmlHelper.XmlDeclaration + '<OK>User Deleted</OK>')

class GetFileList(webapp2.RequestHandler):
  def get(self):
      self.getFileList()

  def post(self):
      self.getFileList()

  def getFileList(self):
      htmlHelper.SetContentTypeToXml(self)
      user = datamodule.GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not user):
          self.response.out.write(htmlHelper.XmlDeclaration + '<error errorId="4">User not found</error>')
          return

      files = datamodule.Files.gql('WHERE user = :1', user).fetch(1000, 0)
      self.response.out.write(htmlHelper.XmlDeclaration + '<files>')
      for file in files:
          self.response.out.write('<file fileName="%s" />' % file.fileName)
      self.response.out.write('</files>')

class AddFile(webapp2.RequestHandler):
  def get(self):
      self.addFile()

  def post(self):
      self.addFile()

  def addFile(self):
      htmlHelper.SetContentTypeToXml(self)
      currUser = datamodule.GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not currUser):
          self.response.out.write(htmlHelper.XmlDeclaration + '<error errorId="4">User not found</error>')
          return

      filename = self.request.get('fileName').strip()

      if len(filename) == 0:
          self.response.out.write(htmlHelper.XmlDeclaration + '<error errorId="5">File name required</error>')
          return

      existingFile = GqlQuery('SELECT __key__ FROM Files WHERE user = :1 AND fileName = :2',
                       currUser,
                       filename).get()
      if existingFile:
          self.response.out.write(htmlHelper.XmlDeclaration + '<error errorId="6">File name not unique</error>')
          return

      file = datamodule.Files(user = currUser,
                   fileName = filename,
                   fileText = self.request.get('fileText'))
      file.put()
      self.response.out.write(htmlHelper.XmlDeclaration + '<OK>File Added</OK>')

class DeleteFile(webapp2.RequestHandler):
  def get(self):
      self.deleteFile()

  def post(self):
      self.deleteFile()

  def deleteFile(self):
      htmlHelper.SetContentTypeToXml(self)
      user = datamodule.GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not user):
          self.response.out.write(htmlHelper.XmlDeclaration + '<error errorId="4">User not found</error>')
          return
      file = datamodule.Files.gql('WHERE user = :1 AND fileName = :2',
                       user,
                       self.request.get('fileName')).get()
      if(not file):
          self.response.out.write(htmlHelper.XmlDeclaration + '<error errorId="7">File not found</error>')
      else:
          file.delete()
          self.response.out.write(htmlHelper.XmlDeclaration + '<OK>File Deleted</OK>')

class GetFileText(webapp2.RequestHandler):
  def get(self):
      self.getFileText()

  def post(self):
      self.getFileText()

  def getFileText(self):
      htmlHelper.SetContentTypeToXml(self)
      user = datamodule.GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not user):
          self.response.out.write(htmlHelper.XmlDeclaration + '<error errorId="4">User not found</error>')
          return
      file = datamodule.Files.gql('WHERE user = :1 AND fileName = :2',
                       user,
                       self.request.get('fileName')).get()
      if(not file):
          self.response.out.write(htmlHelper.XmlDeclaration + '<error errorId="7">File not found</error>')
      else:
          self.response.out.write(file.fileText)

class GetFileAsHTML(webapp2.RequestHandler):
  def get(self):
      self.getFileAsHTML()

  def post(self):
      self.getFileAsHTML()

  def getFileAsHTML(self):
      user = datamodule.GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not user):
          self.response.out.write('<strong style="color:red;">Error: User not found</strong>')
          return
      file = datamodule.Files.gql('WHERE user = :1 AND fileName = :2',
                       user,
                       self.request.get('fileName')).get()
      if(not file):
          self.response.out.write('<strong style="color:red;">Error: File not found</strong>')
      else:
          self.response.out.write(htmlHelper.GetEntriesHTML(file.fileName, file.fileText))

class GetFileListLinks(webapp2.RequestHandler):
  def get(self):
      self.getFileListLinks()

  def post(self):
      self.getFileListLinks()

  def getFileListLinks(self):
      self.response.out.write(htmlHelper.getHtmlFrame() + '''<script type="text/javascript" language="javascript">
                                               function go(funcName, fileName){
                                                   document.forms[0].action='/' + funcName;
                                                   document.forms[0].fileName.value = fileName;
                                                   document.forms[0].submit();
                                               }

                                               function deleteFile(fileName) {
                                                   if(confirm('Are you sure that you wish to permanently delete the file named "' + fileName + '"?')) {
                                                       go('DeleteFile', fileName);
                                                   }
                                               }
                                       </script>
                                 ''')
      user = datamodule.GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not user):
          self.response.out.write('<strong style="color:red;">User not found</strong> - Please check your User Name and Password.</body></html>')
          return

      files = datamodule.Files.gql('WHERE user = :1', user).fetch(1000, 0)

      if(not len(files)):
          self.response.out.write('<span class="header">There are no files on record for this user</span></body></html>')
          return

      self.response.out.write('''<span class="header">List of Files</span>
                                 <form method="post" target="_self">
                                 <input name="userName" type="hidden" value="%s" />
                                 <input name="password" type="hidden" value="%s" />
                                 <input name="fileName" type="hidden" />
                                 <table cellspacing="0" cellpadding="5">''' % (self.request.get('userName'), self.request.get('password')))
      count = 0
      for file in files:
          count += 1
          self.response.out.write('''<tr style="background-color:%s;">
                                         <td>%s.</td>
                                         <td width="400"><span onclick="javascript:go('GetFileAsHTML', '%s');return false;" class="link">%s</span></td>
                                         <td><a class="link" href="" onclick="javascript:deleteFile('%s');return false;">Delete File</a> |
                                             <a class="link" href="" onclick="javascript:go('GetFileText', '%s');return false;">View Source</a></td>
                                     </tr>''' % ('#ffffff' if count % 2 else '#f1f1f1', count, file.fileName, file.fileName, file.fileName, file.fileName))
      self.response.out.write('</table></body></html>')

class GetUsersListLinks(webapp2.RequestHandler):
    def get(self):
        self.getUsersListLinks()

    def post(self):
        self.getUsersListLinks()

    def getUsersListLinks(self):
          self.response.out.write(htmlHelper.getHtmlFrame() + '''<script type="text/javascript" language="javascript">
                                               function go(funcName, userName, password){
                                                   document.forms[0].action='/' + funcName;
                                                   document.forms[0].userName.value = userName;
                                                   document.forms[0].password.value = password;
                                                   document.forms[0].submit();
                                               }

                                               function deleteUser(userName, password) {
                                                   if(confirm('Are you sure that you wish to permanently delete the user "' + userName + '"?')) {
                                                       go('DeleteUser', userName, password);
                                                   }
                                               }
                                       </script>
                                 ''')
          user = datamodule.GetUser(self.request.get('userName'),
                         self.request.get('password'))
          if(not user):
              self.response.out.write('<strong style="color:red;">User not found</strong> - Please check your User Name and Password.</body></html>')
              return

          users = datamodule.Users.all().fetch(1000, 0)

          self.response.out.write('''<span class="header">List of Users</span>
                                     <form method="post" target="_self">
                                     <input name="userName" type="hidden" value="" />
                                     <input name="password" type="hidden" value="" />
                                     <table cellspacing="0" cellpadding="5"><tr class="headRow">
                                        <td></td>
                                        <td>User Name</td>
                                        <td>Password</td>
                                        <td>Is Admin?</td>
                                        <td></td></tr><tr>''')
          count = 0
          for user in users:
              count += 1
              self.response.out.write('''<tr style="background-color:%s;">
                                             <td>%s.</td>
                                             <td>%s</td>
                                             <td>%s</td>
                                             <td>%s</td>
                                             <td><a class="link" onclick="javascript:go('GetFileListLinks', '%s', '%s');return false;">View Files</a> |
                                                 <a class="link" onclick="javascript:deleteUser('%s', '%s');return false;">Delete User</a></td>
                                         </tr>''' % ('#ffffff' if count % 2 else '#f1f1f1', count, user.userName, user.password, str(user.isAdmin), user.userName, user.password, user.userName, user.password,))
          self.response.out.write('</table></body></html>')


class SetFileText(webapp2.RequestHandler):
  def get(self):
      self.setFileText()

  def post(self):
      self.setFileText()

  def setFileText(self):
      htmlHelper.SetContentTypeToXml(self)
      user = datamodule.GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not user):
          self.response.out.write(htmlHelper.XmlDeclaration + '<error errorId="4">User not found</error>')
          return
      file = datamodule.Files.gql('WHERE user = :1 AND fileName = :2',
                       user,
                       self.request.get('fileName')).get()
      if(not file):
          self.response.out.write(htmlHelper.XmlDeclaration + '<error errorId="7">File not found</error>')
      else:
          file.fileText = self.request.get('fileText')
          file.put()
          self.response.out.write(htmlHelper.XmlDeclaration + '<OK>File Updated</OK>')

class GetUsers(webapp2.RequestHandler):
    def get(self):
        self.getUsers()

    def post(self):
        self.getUsers()

    def getUsers(self):
        htmlHelper.SetContentTypeToXml(self)
        users = datamodule.Users.all().fetch(1000, 0)
        self.response.out.write(htmlHelper.XmlDeclaration + '<users>')
        for user in users:
            self.response.out.write('<user userName="' + user.userName + '" password="' + user.password + '" isAdmin="' + str(user.isAdmin) + '" />')
        self.response.out.write('</users>')

class Test(webapp2.RequestHandler):
    def get(self):
        self.test()

    def post(self):
        self.test()

    def test(self):
       htmlHelper.SetContentTypeToXml(self)
       self.response.out.write(htmlHelper.XmlDeclaration + '<NothingHere />')

application = webapp2.WSGIApplication ([('/NewUser', NewUser),
                               ('/DeleteUser', DeleteUser),
                               ('/GetFileList', GetFileList),
                               ('/AddFile', AddFile),
                               ('/DeleteFile', DeleteFile),
                               ('/GetFileText', GetFileText),
                               ('/SetFileText', SetFileText),
                               ('/GetUsers', GetUsers),
                               ('/GetFileAsHTML', GetFileAsHTML),
                               ('/GetFileListLinks', GetFileListLinks),
                               ('/GetUsersListLinks', GetUsersListLinks),
                               ('/Test', Test)], debug = True)
import cgi
from datamodule import Users, Files, GetUser
from google.appengine.ext import webapp
from google.appengine.ext.webapp.util import run_wsgi_app
from google.appengine.ext import db
from google.appengine.ext.db import GqlQuery

XmlDeclaration = '<?xml version="1.0" encoding="utf-8"?>'

def SetContentTypeToXml(handler):
    handler.response.headers['Content-Type'] = 'text/xml'

def getHtmlFrame():
    return '''<html><head>
                   <style type="text/css">
                       body{
                            font-family: arial;
                            padding:0;
                            margin:0;
                       }
                       td{
                           border:solid 1px #d4d4d4;
                           font-size: 11pt;
                       }
                       .header{
                           color: maroon;
                           font-weight: bold;
                           font-size: 11pt;
                        }
                       .link{
                           color: Blue;
                           cursor: pointer;
                       }
                       .link:hover{
                           text-decoration: underline;
                       }
                       </style></head><body>'''

def GetEntriesHTML(fileName, filexml):
    import xml.dom.minidom
    html = getHtmlFrame()
    html += '<span class="header">List of Entries in File: ' + fileName + '</span>'
    html += '<table cellspacing="0" cellpadding="5"><tr><td>&nbsp;</td><td>Date</td><td>Day/Night</td><td>Notes</td></tr>'
    entries = xml.dom.minidom.parseString(filexml.encode( "utf-8" ))
    count = 0
    for entry in entries.getElementsByTagName('Entry'):
        isInvisibleNodes = entry.getElementsByTagName('IsInvisible')
        isInvisible = isInvisibleNodes.length and isInvisibleNodes[0].childNodes[0].data == 'True'
        if not isInvisible:
           count += 1
           date = entry.getElementsByTagName('Date')[0].childNodes[0].data
           dn = ((entry.getElementsByTagName('DN')[0].childNodes[0].data == '1' and 'Day') or 'Night')
           notesNode = entry.getElementsByTagName('Notes')[0].childNodes
           notes = ((notesNode.length and notesNode[0].data) or '&nbsp;')
           html += '<tr style="background-color:' + ('#ffffff;' if count % 2 else '#f1f1f1;') + '">'
           html += '<td width="20">%s</td><td>%s</td><td>%s</td><td width="460">%s</td></tr>' % (count, date, dn, notes)
    html += '</table>'
    kavuahs = entries.getElementsByTagName('Kavuah')
    if kavuahs.length:
        count = 0
        html += '''<br /><span class="header">List of Kavuahs in File: %s</span>
            <table cellspacing="0" cellpadding="5"><tr><td>&nbsp;</td><td>Active?</td><td>Type</td>
            <td>Number</td><td>Day/Night</td><td>Cancels?</td><td>Notes</td></tr>''' % (fileName)
        for kavuah in kavuahs:
            count += 1
            active = kavuah.getElementsByTagName('Active')[0].childNodes[0].data
            type = kavuah.getElementsByTagName('KavuahType')[0].childNodes[0].data
            dn = kavuah.getElementsByTagName('DayNight')[0].childNodes[0].data
            number = kavuah.getElementsByTagName('Number')[0].childNodes[0].data
            cancels = kavuah.getElementsByTagName('CancelsOnahBeinanis')[0].childNodes[0].data
            hasNotesNode = kavuah.getElementsByTagName('Notes').length and kavuah.getElementsByTagName('Notes')[0].hasChildNodes()
            notes = ((hasNotesNode and kavuah.getElementsByTagName('Notes')[0].childNodes[0].data) or '&nbsp;')
            html += '<tr style="background-color:' + ('#ffffff;' if count % 2 else '#f1f1f1;') + '">'
            html += '''<td width="20">%s</td><td>%s</td><td>%s</td><td>%s</td><td>%s</td>
                <td>%s</td><td width="460">%s</td>
                </tr>''' % (count, 'Yes' if active == 'true' else 'No', type, number, dn, 'Yes' if cancels == 'true' else 'No', notes)
        html += '</table>'
    html += '</body></html>'
    return html;

class NewUser(webapp.RequestHandler):
    def post(self):
        SetContentTypeToXml(self)
        userName = self.request.get('userName').strip()
        password = self.request.get('password').strip()
        isAdminUser = self.request.get('isAdmin')

        if(len(userName) < 2):
           self.response.out.write(XmlDeclaration + '<error errorId="1">Invalid username</error>')
           return
        if(len(password) < 4):
           self.response.out.write(XmlDeclaration + '<error errorId="2">Invalid password</error>')
           return
        if(GetUser(userName, password)):
           self.response.out.write(XmlDeclaration + '<error errorId="3">Username and password already taken</error>')
           return

        user = Users(userName = userName, password = password, isAdmin = (True if isAdminUser else False))
        user.put()
        self.response.out.write(XmlDeclaration + '<OK>User Created</OK>')

class DeleteUser(webapp.RequestHandler):
  def post(self):
      SetContentTypeToXml(self)
      user = GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not user):
          self.response.out.write(XmlDeclaration + '<error errorId="4">User not found</error>')
          return

      files = GqlQuery('SELECT __key__ FROM Files WHERE user = :1', user)
      db.delete(files.fetch(1000, 0))
      user.delete()
      self.response.out.write(XmlDeclaration + '<OK>User Deleted</OK>')

class GetFileList(webapp.RequestHandler):
  def post(self):
      SetContentTypeToXml(self)
      user = GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not user):
          self.response.out.write(XmlDeclaration + '<error errorId="4">User not found</error>')
          return

      files = Files.gql('WHERE user = :1', user).fetch(1000, 0)
      self.response.out.write(XmlDeclaration + '<files>')
      for file in files:
          self.response.out.write('<file fileName="%s" />' % file.fileName)
      self.response.out.write('</files>')

class AddFile(webapp.RequestHandler):
  def post(self):
      SetContentTypeToXml(self)
      currUser = GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not currUser):
          self.response.out.write(XmlDeclaration + '<error errorId="4">User not found</error>')
          return

      filename = self.request.get('fileName').strip()

      if len(filename) == 0:
          self.response.out.write(XmlDeclaration + '<error errorId="5">File name required</error>')
          return

      existingFile = GqlQuery('SELECT __key__ FROM Files WHERE user = :1 AND fileName = :2',
                       currUser,
                       filename).get()
      if existingFile:
          self.response.out.write(XmlDeclaration + '<error errorId="6">File name not unique</error>')
          return

      file = Files(user = currUser,
                   fileName = filename,
                   fileText = self.request.get('fileText'))
      file.put()
      self.response.out.write(XmlDeclaration + '<OK>File Added</OK>')

class DeleteFile(webapp.RequestHandler):
  def post(self):
      SetContentTypeToXml(self)
      user = GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not user):
          self.response.out.write(XmlDeclaration + '<error errorId="4">User not found</error>')
          return
      file = Files.gql('WHERE fileName = :1', self.request.get('fileName')).get()
      if(not file):
          self.response.out.write(XmlDeclaration + '<error errorId="7">File not found</error>')
      else:
          file.delete()
          self.response.out.write(XmlDeclaration + '<OK>File Deleted</OK>')

class GetFileText(webapp.RequestHandler):
  def post(self):
      SetContentTypeToXml(self)
      user = GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not user):
          self.response.out.write(XmlDeclaration + '<error errorId="4">User not found</error>')
          return
      file = Files.gql('WHERE user = :1 AND fileName = :2',
                       user,
                       self.request.get('fileName')).get()
      if(not file):
          self.response.out.write(XmlDeclaration + '<error errorId="7">File not found</error>')
      else:
          self.response.out.write(file.fileText)

class GetFileAsHTML(webapp.RequestHandler):
  def post(self):
      user = GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not user):
          self.response.out.write('<strong style="color:red;">Error: User not found</strong>')
          return
      file = Files.gql('WHERE user = :1 AND fileName = :2',
                       user,
                       self.request.get('fileName')).get()
      if(not file):
          self.response.out.write('<strong style="color:red;">Error: File not found</strong>')
      else:
          self.response.out.write(GetEntriesHTML(file.fileName, file.fileText))

class GetFileListLinks(webapp.RequestHandler):
  def post(self):
      self.response.out.write(getHtmlFrame() + '''<script type="text/javascript" language="javascript">
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
      user = GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not user):
          self.response.out.write('<strong style="color:red;">User not found</strong> - Please check your User Name and Password.</body></html>')
          return

      files = Files.gql('WHERE user = :1', user).fetch(1000, 0)

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
                                         <td><a href="" onclick="javascript:deleteFile('%s');return false;">Delete File</a> |
                                             <a href="" onclick="javascript:go('GetFileText', '%s');return false;">View Source</a></td>
                                     </tr>''' % ('#ffffff' if count % 2 else '#f1f1f1', count, file.fileName, file.fileName, file.fileName, file.fileName))
      self.response.out.write('</table></body></html>')

class SetFileText(webapp.RequestHandler):
  def post(self):
      SetContentTypeToXml(self)
      user = GetUser(self.request.get('userName'),
                     self.request.get('password'))
      if(not user):
          self.response.out.write(XmlDeclaration + '<error errorId="4">User not found</error>')
          return
      file = Files.gql('WHERE user = :1 AND fileName = :2',
                       user,
                       self.request.get('fileName')).get()
      if(not file):
          self.response.out.write(XmlDeclaration + '<error errorId="7">File not found</error>')
      else:
          file.fileText = self.request.get('fileText')
          file.put()
          self.response.out.write(XmlDeclaration + '<OK>File Updated</OK>')

class GetUsers(webapp.RequestHandler):
    def post(self):
        SetContentTypeToXml(self)
        users = Users.all().fetch(1000, 0)
        self.response.out.write(XmlDeclaration + '<users>')
        for user in users:
            self.response.out.write('<user userName="' + user.userName + '" password="' + user.password + '" isAdmin="' + str(user.isAdmin) + '" />')
        self.response.out.write('</users>')

class Test(webapp.RequestHandler):
    def post(self):
       SetContentTypeToXml(self)
       self.response.out.write(XmlDeclaration + '<NothingHere />')

if __name__ == "__main__":
  run_wsgi_app(webapp.WSGIApplication([('/NewUser', NewUser),
                                       ('/DeleteUser', DeleteUser),
                                       ('/GetFileList', GetFileList),
                                       ('/AddFile', AddFile),
                                       ('/DeleteFile', DeleteFile),
                                       ('/GetFileText', GetFileText),
                                       ('/SetFileText', SetFileText),
                                       ('/GetUsers', GetUsers),
                                       ('/GetFileAsHTML', GetFileAsHTML),
                                       ('/GetFileListLinks', GetFileListLinks),
                                       ('/Test', Test)],
                                     debug=True))
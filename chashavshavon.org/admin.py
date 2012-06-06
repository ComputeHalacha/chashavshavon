from datamodule import GetAdminUserKey, GetAdminUser
from google.appengine.ext import webapp
from google.appengine.ext.webapp.util import run_wsgi_app

class DisplayAdmin(webapp.RequestHandler):
    adminPageHtml='''<html>
            <head>
                <title>Chash - Admin Console</title>

                <script type="text/javascript" language="JavaScript">
                    if (!Object.prototype.isIn) {
                        Object.prototype.isIn = function() {
                            for (var i = 0; i < arguments.length; i++) {
                                if (arguments[i] == this) {
                                    return true;
                                }
                            }
                            return false;
                        }
                    }

                    function runQuery(functionName) {
                        if (hasValidFields(functionName)) {
                            document.forms[0].action = '/' + functionName;
                            document.forms[0].submit();
                        }
                        else {
                            frames['iframeResults'].location = 'about:blank';
                        }
                    }

                    function hasValidFields(functionName) {
                        if (functionName.isIn('GetUsers', 'Test')) {
                            message();
                            return true;
                        }

                        var txt = '';
                        if (document.forms[0].userName.value.length < 2) {
                            txt += '<br />* User name must contain at least 2 characters.';
                        }
                        if (document.forms[0].password.value.length < 4) {
                            txt += '<br />* Password must contain at least 4 characters.';
                        }
                        if (!document.forms[0].fileName.value
            				&& functionName.isIn('AddFile', 'SetFileText')) {
                            txt += '<br />* File name needs to be supplied.';
                        }
                        message(txt);
                        return txt.length == 0;
                    }

                    function message(txt) {
                        document.getElementById('tdMessage').innerHTML = (txt ? '<strong>NOTE:</strong>' + txt : '');
                        document.getElementById('tdMessage').style.borderWidth = txt ? '1px' : '0';
                    }
                </script>

                <style type="text/css">
                    body, tr, input, textarea, select
                    {
                        font-family: arial;
                        font-size: 8pt;
                    }
                    tr, input, textarea, select
                    {
                        border: solid 1px silver;
                        vertical-align: top;
                    }
                    .Link
                    {
                        color: Blue;
                        cursor: pointer;
                        text-decoration: none;
                    }
                    .Link:hover
                    {
                        text-decoration: underline;
                    }
                    .Message
                    {
                        color: #FF0000;
                        font-size: 7pt;
                        border: solid 0 #FF0000;
                        padding: 5px;
                    }
                </style>
                <link rel="icon" type="image/png" href="/static/chash.png" />
                <link rel="shortcut icon" href="/static/chash.ico" />
            </head>
            <body style="padding: 0; margin: 0;">
                <img src="http://code.google.com/appengine/images/appengine-silver-120x30.gif" style="float:right;margin:3px;" alt="Powered by Google App Engine" />
                <form method="post" target="iframeResults">
                <table height="100%" style="padding: 0; margin: 0;" cellpadding="10">
                    <tr>
                        <td style="background-color: #f1f1f1;width:210px;">
                            <img src="/static/Scroll.png" height="20" style="float: left;margin:0 5px 5px 0;" alt="Chashavshavon" />
                            <div style="height: 25px; color: Maroon; font-size: 9pt; font-weight: bold;">
                                &nbsp;Chashavshavon Admin Console
                            </div>
                            <table>
                                <tr>
                                    <td colspan="2" style="color: darkblue; font-weight: bold;">
                                        <hr />
                                        Fill in the appropriate fields:
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        User Name:
                                    </td>
                                    <td>
                                        <input type="text" name="userName" title="Must contain at least 2 characters" onblur="javascript:if(this.value.length < 2)message('<br />* User name must contain at least 2 characters');"
                                            onkeypress="javascript:message();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password:
                                    </td>
                                    <td>
                                        <input type="text" name="password" title="Must contain at least 4 characters" onblur="javascript:if(this.value.length < 2)message('<br />* Password must contain at least 4 characters');"
                                            onkeypress="javascript:message();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        File Name:
                                    </td>
                                    <td>
                                        <input type="text" name="fileName" onblur="javascript:if(!this.value)message('<br />* File name must contain at least 1 character');"
                                            onkeypress="javascript:message();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        File Text:
                                    </td>
                                    <td>
                                        <textarea cols="20" rows="5" name="fileText"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" id="tdMessage" class="Message">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="color: darkblue; font-weight: bold;">
                                        <hr />
                                        Select a function:
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        *
                                        <span class="Link" onclick="javascript:runQuery('GetUsersListLinks');return false;">
                                            Get List of Users</span><br />
                                        *
                                        <span class="Link" onclick="javascript:runQuery('NewUser');return false;">
                                            Create New User</span><br />
                                        *
                                        <span class="Link" onclick="javascript:runQuery('DeleteUser');return false;">
                                            Delete User</span><br />
                                        *
                                        <span class="Link" onclick="javascript:runQuery('GetFileListLinks');return false;">
                                            Get List of Files</span><br />
                                        *
                                        <span class="Link" onclick="javascript:runQuery('AddFile');return false;">
                                            Add New file</span><br />
                                        *
                                        <span class="Link" onclick="javascript:runQuery('SetFileText');return false;">
                                            Change File Text</span><br />
                                        *
                                        <span class="Link" onclick="javascript:runQuery('Test');return false;">
                                            Test</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="padding-left: 20px;">
                            <iframe name="iframeResults" width="700" height="100%" scrolling="auto" frameborder="0">
                            </iframe>
                        </td>
                    </tr>
                </table>
                </form>
            </body>
            </html>'''
    def get(self):
        ekey = self.request.cookies.get('ekey')
        if(ekey):
            user = GetAdminUser(ekey)
        if(not ekey or not user):
            self.redirect("/static/adminlogin.html?BLI=1")
        else:
            self.response.out.write(self.adminPageHtml)

    def post(self):
       user = GetAdminUserKey(self.request.get('userName'), self.request.get('password'))
       if(not user):
           self.redirect("/static/adminlogin.html?BLI=1")
       else:
           if(self.request.get('setCookie')):
               import datetime
               expiresDate = datetime.datetime.utcnow() + datetime.timedelta(365)
               self.response.headers.add_header('Set-Cookie', 'ekey=%s; expires=%s' % (user, expiresDate.strftime('%d %b %Y %H:%M:%S GMT')))
           self.response.out.write(self.adminPageHtml)
if __name__ == "__main__":
    run_wsgi_app(webapp.WSGIApplication([('/[A|a]dmin', DisplayAdmin)]))
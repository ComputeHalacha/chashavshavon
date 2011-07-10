from google.appengine.ext import webapp
from google.appengine.ext.webapp.util import run_wsgi_app

class LowerCaseRedirecter(webapp.RequestHandler):
    def get(self, path):
        self.redirect('/static/%s' % (path.lower(),))

if __name__ == "__main__":
    run_wsgi_app(webapp.WSGIApplication([('/static/(.*)', LowerCaseRedirecter)]))
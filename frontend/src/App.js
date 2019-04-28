import React from 'react';
import AppNavbar from './components/AppNavbar';
import Registrations from './components/Registrations';

import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';

function App() {
  return (
    <div className="App">
      <AppNavbar/>
        <Registrations/>
    </div>
  );
}

export default App;

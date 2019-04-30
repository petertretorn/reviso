import React from 'react';
import Navbar from './components/Navbar';
import Registrations from './components/Container/Registrations';

import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';

function App() {
  return (
    <div className="App">
        <Navbar/>
        <Registrations/>
    </div>
  );
}

export default App;

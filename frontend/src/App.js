import React from 'react';
import Navbar from './components/View/Navbar';
import DashBoard from './components/Container/DashBoard';

import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';

function App() {
  return (
    <div className="App">
        <Navbar/>
        <DashBoard/>
    </div>
  );
}

export default App;

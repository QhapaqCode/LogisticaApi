import './App.css';
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import AlmacenList from './components/AlmacenList';
import AlmacenForm from './components/AlmacenForm';
import TopBar from './components/TopBar';
import AlmacenUpdate from './components/AlmacenUpdate';
import AlmacenDelete from './components/AlmacenDelete';

function App() {
  return (
    <Router>
      <div>
        <TopBar />
        <Routes>
          <Route path="/" element={<AlmacenList />} />
          <Route path="/almacen-list" element={<AlmacenList />} />
          <Route path="/almacen-form" element={<AlmacenForm />} />
          <Route path="/almacen-update" element={<AlmacenUpdate />} />
          <Route path="/almacen-delete" element={<AlmacenDelete />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;

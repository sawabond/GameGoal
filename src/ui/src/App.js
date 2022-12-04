import React, { useState, useMemo } from 'react';
import { Route, Routes } from 'react-router-dom';
import Home from './pages/Home/Home';
import Registration from './pages/Registration/Registration';
import './App.scss';
import Login from './pages/Login/Login';
import { AuthContext } from './hooks/useAuth';
import ImportMembers from './pages/ImportMembers/ImportMembers';
function App() {
  const [user, setUser] = useState(null);
  const providerUser = useMemo(() => ({ user, setUser }), [user, setUser]);
  return (
    <div className="App">
      <AuthContext.Provider value={providerUser}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/registr" element={<Registration />} />
          <Route path="/login" element={<Login />} />
          <Route path="/import" element={<ImportMembers />} />
        </Routes>
      </AuthContext.Provider>
    </div>
  );
}

export default App;

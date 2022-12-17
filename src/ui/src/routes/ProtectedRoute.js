import React from 'react';
import { useContext } from 'react';
import { Outlet } from 'react-router';
import { AuthContext } from '../hooks/useAuth';
import Error from '../pages/Error/Error';
export const ProtectedRoute = ({ children }) => {
  const { user } = useContext(AuthContext);
  if (!user) {
    return <Error />;
  }
  return <Outlet />;
};

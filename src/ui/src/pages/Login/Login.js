import React, { useContext } from 'react';
import { CssVarsProvider } from '@mui/joy/styles';
import Sheet from '@mui/joy/Sheet';
import Typography from '@mui/joy/Typography';
import TextField from '@mui/joy/TextField';
import Button from '@mui/joy/Button';
import Header from '../../components/Header';
import { useFormik } from 'formik';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { AuthContext } from '../../hooks/useAuth';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export default function Login() {
  const { user, setUser } = useContext(AuthContext);

  const navigate = useNavigate();

  const formik = useFormik({
    initialValues: {
      UserName: '',
      Password: '',
    },
    onSubmit: (values, { resetForm }) => {
      axios
        .post(
          `https://localhost:7184/api/User/login?UserName=${values.UserName}&Password=${values.Password}`,
          {
            UserName: values.UserName,
            Password: values.Password,
          }
        )
        .then((response) => {
          if (response.status === 200) {
            toast.success('You are logged in');
            navigate('/');
          }
          setUser(() => ({
            ...response.data,
          }));
        })
        .catch(function (error) {
          const errorJSON = error.toJSON();
          if (errorJSON.status === 400) {
            toast.warning('Wrong password or login');
          }
        });
    },
  });
  sessionStorage.setItem('user', JSON.stringify(user));
  return (
    <>
      <Header />
      <CssVarsProvider>
        <main>
          <ToastContainer />
          <Sheet
            sx={{
              maxWidth: 400,
              mx: 'auto', // margin left & right
              my: 4, // margin top & botom
              py: 3, // padding top & bottom
              px: 2, // padding left & right
              display: 'flex',
              justifyContent: 'center',
              flexDirection: 'column',
              gap: 2,
              borderRadius: 'sm',
              boxShadow: 'md',
            }}
            variant="outlined"
          >
            <div>
              <Typography level="h4" component="h1">
                <b>Welcome!</b>
              </Typography>
              <Typography level="body2">Sign in to continue.</Typography>
            </div>
            <form onSubmit={formik.handleSubmit}>
              <TextField
                name="UserName"
                label="Username"
                onChange={formik.handleChange}
                value={formik.values.UserName}
              />
              <TextField
                name="Password"
                type="password"
                placeholder="password"
                label="Password"
                onChange={formik.handleChange}
                value={formik.values.Password}
              />
              <Button
                sx={{
                  mt: 1, // margin top
                }}
                type="submit"
              >
                Log in
              </Button>
              <Typography fontSize="sm" sx={{ alignSelf: 'center' }}>
                Don&apos;t have an account?
                <Link to={'/registr'}>Sign up</Link>
              </Typography>
            </form>
          </Sheet>
        </main>
      </CssVarsProvider>
    </>
  );
}

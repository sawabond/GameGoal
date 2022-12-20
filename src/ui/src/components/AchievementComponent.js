import * as React from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import { Button, CardActionArea, CardActions } from '@mui/material';
export default function AchievementComponent({ achievement }) {
  return (
    <Card sx={{ maxWidth: 345 }}>
      <CardActionArea>
        <CardMedia
          component="img"
          image={require('../img/no-image.jpg')}
          height="140"
          alt="img"
        />
        <CardContent>
          <Typography gutterBottom variant="h5" component="div">
            {achievement.name}
          </Typography>
          <Typography variant="body2" color="text.secondary">
            {achievement.description}
          </Typography>
        </CardContent>
      </CardActionArea>
      <CardActions>
        {/* <Button size="small" color="primary">
          Share
        </Button> */}
      </CardActions>
    </Card>
  );
}
